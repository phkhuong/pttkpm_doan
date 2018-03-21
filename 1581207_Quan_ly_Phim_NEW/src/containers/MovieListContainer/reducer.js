import { REQUEST_MOVIES, REQUEST_MOVIES_SUCCESS, REQUEST_MOVIES_FAIL, SELECT_MOVIE, SELECT_MOVIE_SUCCESS, USER_LOGIN_SUCCESS, USER_LOGIN_FAIL, USER_LOGIN_REQUEST } from "./constants";

const initialState = {
    movies: [],
    selectedMovie: {},
    error: '',
    user: {},
    isLoggedIn: false,
};

function movieListContainerReducer(state = initialState, action) {
    switch (action.type) {
        case REQUEST_MOVIES_SUCCESS:
            return Object.assign({}, state, { movies: action.movies });
        case REQUEST_MOVIES_FAIL:
            return { ...state, error: action.error };
        case SELECT_MOVIE_SUCCESS:
            // const mv = state.movies.find((mv) => mv.Ma_so == action.id);
            return { ...state, selectedMovie: action.selectedMovie };
        case USER_LOGIN_SUCCESS: 
            return { ...state, user: action.user, isLoggedIn: true, error:''};
        case USER_LOGIN_FAIL: 
            return { ...state, isLoggedIn: false, error: action.error};
        default:
            return state;
    }
}

export default movieListContainerReducer;
