import { findMaxSessionID, defaultSeats } from "../../utils";
import { REQUEST_MOVIES, 
    REQUEST_MOVIES_SUCCESS, 
    REQUEST_MOVIES_FAIL, 
    SELECT_MOVIE, 
    SELECT_MOVIE_SUCCESS, 
    USER_LOGIN_SUCCESS, 
    USER_LOGIN_FAIL, 
    USER_LOGIN_REQUEST,
    REQUEST_CINEMAS,
    REQUEST_CINEMAS_SUCCESS,
    REQUEST_CINEMAS_FAIL,
    DELETE_SESSION,
    CREATE_SESSION,
    ADD_MOVIE_SUCCESS,
    UPDATE_MOVIE_SUCCESS,
    UPDATE_MOVIE_FAIL,
    ADD_MOVIE_FAIL,
    DELETE_MOVIE_SUCCESS,
    DELETE_MOVIE_FAIL,
    RESET_API_RESULT,
} from "./constants";



const initialState = {
    movies: [],
    cinemas: [],
    selectedMovie: {},
    error: '',
    user: {},
    isLoggedIn: false,
    success: '',
};

function movieListContainerReducer(state = initialState, action) {
    switch (action.type) {
        case REQUEST_MOVIES_SUCCESS:
            return Object.assign({}, state, { movies: action.movies });
        case REQUEST_MOVIES_FAIL:
            return { ...state, error: action.error };
        case REQUEST_CINEMAS_SUCCESS:
            return Object.assign({}, state, { cinemas: action.cinemas });
        case REQUEST_CINEMAS_FAIL:
            return { ...state, error: action.error };
        case SELECT_MOVIE_SUCCESS:
            // const mv = state.movies.find((mv) => mv.Ma_so == action.id);
            return { ...state, selectedMovie: action.selectedMovie };
        case USER_LOGIN_SUCCESS: 
            return { ...state, user: action.user, isLoggedIn: true, error:''};
        case USER_LOGIN_FAIL: 
            return { ...state, isLoggedIn: false, error: action.error};
        case DELETE_SESSION:
            let mv = state.movies.find(m => m.Ma_so == action.movieID);
            const sessions = mv["Danh_sach_Suat_chieu"].filter(s => {
                if (s.Ma_so != action.sessionID)
                    return s;
            });
            mv.Danh_sach_Suat_chieu = sessions;
            return {
                ...state, 
                movies: [
                    ...state.movies.filter(m => m.Ma_so != action.movieID),
                    mv,
                ],
            }
        case CREATE_SESSION:
            // create session ID
            let newSession = action.session;
            let mv1 = state.movies.find(m => m.Ma_so == action.movieID);
            
            newSession.Ma_so = 'S_' + (findMaxSessionID(mv1.Danh_sach_Suat_chieu)+1);
            newSession.Danh_sach_Ghe_trong = defaultSeats;

            mv1.Danh_sach_Suat_chieu = mv1["Danh_sach_Suat_chieu"].concat(newSession);

            return {
                ...state,
                movies: [
                    ...state.movies.filter(m => m.Ma_so != action.movieID),
                    mv1,
                ],
            }
        case UPDATE_MOVIE_SUCCESS:  // update store movies ?
            return {
                ...state,
                error: '',
                success: action.message,
                movies: [
                    ...state.movies.filter(m => m.Ma_so != action.movie.Ma_so),
                    action.movie
                ]
            }
        case UPDATE_MOVIE_FAIL:
            return {
                ...state,
                error: action.error,
                success: '',
            }
        case ADD_MOVIE_SUCCESS:
            return {
                ...state,
                error: '',
                success: action.message,
                movies: [
                    ...state.movies.filter(m => m.Ma_so != action.movie.Ma_so),
                    action.movie
                ]
            }
        case ADD_MOVIE_FAIL:
            return {
                ...state,
                error: action.error,
                success: '',
            }
        case DELETE_MOVIE_SUCCESS:
            return {
                ...state,
                error: '',
                success: action.message,
                movies: [
                    ...state.movies.filter(m => m.Ma_so != action.movieID)
                ]
            }
        case DELETE_MOVIE_FAIL:
            return {
                ...state,
                error: action.error,
                success: '',
            }
        case RESET_API_RESULT:
            return {
                ...state,
                error: '',
                success: '',
            }
        default:
            return state;
    }
}



export default movieListContainerReducer;
