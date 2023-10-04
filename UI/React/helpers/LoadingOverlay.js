import React from 'react';
import SpinnerOverlay from 'react-native-loading-spinner-overlay';

const LoadingOverlay = ({ isVisible }) => {
  return (
    <SpinnerOverlay
      visible={isVisible}
      textContent={'Loading...'}
      textStyle={{ color: '#FFF' }}
    />
  );
};

export default LoadingOverlay;
