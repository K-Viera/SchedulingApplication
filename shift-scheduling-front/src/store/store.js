import { configureStore } from '@reduxjs/toolkit';
import authReducer from './authSlice';

const saveAuthToken = (state) => {
  try{
    const serializedState = JSON.stringify(state);
    localStorage.setItem('state', serializedState);
  }
  catch(e){
    console.error('Error saving state:', e);
  }
};

const loadAuthToken = () => {
  try{
    const serializedState = localStorage.getItem('state');
    if(serializedState === null){
      return undefined;
    }
    return JSON.parse(serializedState);
  }
  catch(e){
    console.error('Error loading state:', e);
    return undefined;
  }
};
const preloadedState ={
  auth: loadAuthToken(),
};

export const store = configureStore({
  reducer: {
    auth: authReducer,
  },
  preloadedState,
});


store.subscribe(() => {
  saveAuthToken(store.getState().auth);
});