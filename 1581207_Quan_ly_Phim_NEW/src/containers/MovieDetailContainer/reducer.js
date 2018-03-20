import { SELECT_MOVIE } from "./constants";

const initialState = {
    movie: {},
    // error: '',
};

function movieDetailContainerReducer(state = initialState, action) {
    switch (action.type) {
        case SELECT_MOVIE:
            return { movie: action.movie };
        
        default:
            return state;
    }
}

export default movieDetailContainerReducer;
