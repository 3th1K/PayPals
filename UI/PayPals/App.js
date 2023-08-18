import { StyleSheet, Text, View } from 'react-native';
import Navigation from './StackNavigator';
import { UserProvider } from './helpers/UserContext';
import { Provider } from 'react-redux';
import { Store } from './redux/Store';

export default function App() {
  return (
    <Provider store={Store}>
      <Navigation/>
    </Provider>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
