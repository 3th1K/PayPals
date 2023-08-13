import { logger, consoleTransport } from "react-native-logs";

const defaultConfig = {
    levels: {
        debug: 0,
        info: 1,
        success: 2,
        warn: 3,
        error: 4,
    },
    severity: "debug",
    transport: consoleTransport,
    transportOptions: {
        colors: {
            info: "blueBright",
            success: "greenBright",
            warn: "yellowBright",
            error: "redBright",
        },
        extensionColors: {
            
        },
    },
    async: true,
    dateFormat: "time",
    printLevel: true,
    printDate: true,
    enabled: true,
};

export const CreateLogger = (moduleName) => {
    return logger.createLogger(defaultConfig).extend(moduleName);
};
