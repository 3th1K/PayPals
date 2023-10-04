using System.Reflection;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using FluentValidation;
using MediatR;

namespace Common.Validations
{
    public class TypeInformation
    {
        public Type? Type { get; set; }
        public Type[]? GenericArguments { get; set; }
    }


    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IValidator<TRequest> _validators;
        private readonly IExceptionHandler _exceptionHandler;

        public ValidationBehavior(IValidator<TRequest> validators, IExceptionHandler exceptionHandler)
        {
            _validators = validators;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validators.ValidateAsync(request, cancellationToken);
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ApiResult<>))
            {
                TypeInformation? info = GetClassAndGenericArguments<TResponse>();
                object instance = CreateGenericInstance(info.Type, info.GenericArguments);

                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    object r1 = InvokeMethod(instance, "Failure", new object[] { ErrorType.ErrValidationFailed, "Request Validation Failed", validationErrors });
                    return (TResponse)r1;
                }

                try
                {
                    return await next();
                }
                catch (ValidationException ex)
                {
                    var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                    object r1 = InvokeMethod(instance, "Failure", new object[] { ErrorType.ErrValidationFailed, "Request Validation Failed", validationErrors });
                    return (TResponse)r1;
                }
                catch (Exception ex)
                {
                    object r1 = InvokeMethod(instance, "Failure", new object[] { ex });
                    return (TResponse)r1;
                }

            }
            else
            {
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    throw new ValidationException(validationErrors.ToString());
                }
                var x = await _exceptionHandler.HandleException<TResponse>(async () => {
                    return await next();
                });
                if (!x.Success)
                {
                    throw new ApiException(x.ErrorObject);
                }
                return x.Result;

            }
        }

        public static TypeInformation? GetClassAndGenericArguments<TResponse>()
        {
            Type type = typeof(TResponse);

            // Check if it's a generic type
            if (type.IsGenericType)
            {
                // Get the generic type definition (e.g., List<>, Dictionary<,>, etc.)
                Type genericTypeDefinition = type.GetGenericTypeDefinition();

                // Get the generic arguments (e.g., int in List<int>, TKey and TValue in Dictionary<TKey, TValue>, etc.)
                Type[] genericArguments = type.GetGenericArguments();

                return new TypeInformation
                {
                    Type = genericTypeDefinition,
                    GenericArguments = genericArguments
                };
            }

            return null;
            //else
            //{
            //    // Not a generic type, cannot create an instance
            //    //throw new InvalidOperationException("TResponse is not a generic type.");
            //}
        }

        public static object CreateGenericInstance(Type classType, Type[] genericArguments)
        {
            if (classType == null)
            {
                throw new ArgumentNullException(nameof(classType));
            }

            // Check if it's a generic type
            if (classType.IsGenericType)
            {
                // Construct the specific generic type by applying the generic arguments
                Type constructedType = classType.MakeGenericType(genericArguments);

                // Create an instance of the constructed generic type
                return Activator.CreateInstance(constructedType);
            }
            else
            {
                // Not a generic type, cannot create an instance
                throw new InvalidOperationException("classType is not a generic type.");
            }
        }

        public static object InvokeMethod(object instance, string methodName, object[] methodArguments)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            Type instanceType = instance.GetType();

            // Specify the types of the method parameters
            Type[] methodArgumentTypes = methodArguments?.Select(arg => arg.GetType()).ToArray() ?? Type.EmptyTypes;

            // Find the method with the specified name and parameter types
            MethodInfo methodInfo = instanceType.GetMethod(methodName, methodArgumentTypes);

            if (methodInfo != null)
            {
                return methodInfo.Invoke(instance, methodArguments);
            }
            else
            {
                throw new InvalidOperationException($"Method '{methodName}' not found in the type '{instanceType.FullName}' with the specified parameter types.");
            }
        }

        
    }

}
