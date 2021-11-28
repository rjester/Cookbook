import { useState, useEffect, useReducer } from 'react';
import axios from 'axios';

// https://www.robinwieruch.de/react-hooks-fetch-data
const dataFetchReducer = (state, action) => {
    switch (action.type) {
      case 'FETCH_INIT':
        return { ...state, isLoading: true, isError: false };
      case 'FETCH_SUCCESS':
        return {
          ...state,
          isLoading: false,
          isError: false,
          data: action.payload,
        };
  
      case 'FETCH_FAILURE':
        return { ...state, isLoading: false, isError: true };
      default:
        throw new Error();
    }
  };

  const useCookbookApi = (initialUrl, initialData) => {
    const [url, setUrl] = useState(initialUrl);
  
    const [state, dispatch] = useReducer(dataFetchReducer, {
      isLoading: false,
      isError: false,
      data: initialData,
    });
  
    useEffect(() => {
      let didCancel = false;
      const fetchRecipes = async () => {
        dispatch({ type: 'FETCH_INIT' });
  
        try {
          console.log('calling fetch 11');
            const result = await axios(url);
  
          if (!didCancel) {
            dispatch({ type: 'FETCH_SUCCESS', payload: result.data });
          }
        } catch (error) {
          if (!didCancel) {
            dispatch({ type: 'FETCH_FAILURE' });
          }
        }
      };
  
      fetchRecipes();
  
      return () => {
        didCancel = true;
      };
    }, [url]);
  
    return [state, setUrl];
  };

  export default useCookbookApi;