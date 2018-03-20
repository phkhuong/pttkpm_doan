import { INCREASE } from './constants';


const initialState = {
  counter: 0,
};

function counterContainerReducer(state = initialState, action) {
  switch (action.type) {
    case INCREASE:
      // return {counter: state.counter+1};
      return Object.assign({}, ...state, {counter: state.counter+1});
    default:
      return state;
  }
}

export default counterContainerReducer;
