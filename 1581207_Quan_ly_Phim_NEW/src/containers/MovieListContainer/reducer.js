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
    CREATE_SESSION
} from "./constants";

const defaultSeats = [
    {"Ma_so":'A1'}, {"Ma_so":'A2'}, {"Ma_so":'A3'}, {"Ma_so":'A4'}, {"Ma_so":'A5'}, {"Ma_so":'A6'}, {"Ma_so":'A7'}, {"Ma_so":'A8'}, {"Ma_so":'A9'},
    {"Ma_so":'B1'}, {"Ma_so":'B2'}, {"Ma_so":'B3'}, {"Ma_so":'B4'}, {"Ma_so":'B5'}, {"Ma_so":'B6'}, {"Ma_so":'B7'}, {"Ma_so":'B8'}, {"Ma_so":'B9'},
    {"Ma_so":'C1'}, {"Ma_so":'C2'}, {"Ma_so":'C3'}, {"Ma_so":'C4'}, {"Ma_so":'C5'}, {"Ma_so":'C6'}, {"Ma_so":'C7'}, {"Ma_so":'C8'}, {"Ma_so":'C9'},
    {"Ma_so":'D1'}, {"Ma_so":'D2'}, {"Ma_so":'D3'}, {"Ma_so":'D4'}, {"Ma_so":'D5'}, {"Ma_so":'D6'}, {"Ma_so":'D7'}, {"Ma_so":'D8'}, {"Ma_so":'D9'},
    {"Ma_so":'E1'}, {"Ma_so":'E2'}, {"Ma_so":'E3'}, {"Ma_so":'E4'}, {"Ma_so":'E5'}, {"Ma_so":'E6'}, {"Ma_so":'E7'}, {"Ma_so":'E8'}, {"Ma_so":'E9'},
    {"Ma_so":'F1'}, {"Ma_so":'F2'}, {"Ma_so":'F3'}, {"Ma_so":'F4'}, {"Ma_so":'F5'}, {"Ma_so":'F6'}, {"Ma_so":'F7'}, {"Ma_so":'F8'}, {"Ma_so":'F9'},
    {"Ma_so":'G1'}, {"Ma_so":'G2'}, {"Ma_so":'G3'}, {"Ma_so":'G4'}, {"Ma_so":'G5'}, {"Ma_so":'G6'}, {"Ma_so":'G7'}, {"Ma_so":'G8'}, {"Ma_so":'G9'},
    {"Ma_so":'H1'}, {"Ma_so":'H2'}, {"Ma_so":'H3'}, {"Ma_so":'H4'}, {"Ma_so":'H5'}, {"Ma_so":'H6'}, {"Ma_so":'H7'}, {"Ma_so":'H8'}, {"Ma_so":'H9'},
    {"Ma_so":'I1'}, {"Ma_so":'I2'}, {"Ma_so":'I3'}, {"Ma_so":'I4'}, {"Ma_so":'I5'}, {"Ma_so":'I6'}, {"Ma_so":'I7'}, {"Ma_so":'I8'}, {"Ma_so":'I9'},
]; 

const initialState = {
    movies: [],
    cinemas: [],
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
        default:
            return state;
    }
}

function findMaxSessionID(sessions) {
    if(sessions.length == 0)
        return 0;
    let max = sessions.reduce((pre, cur) =>{
        if (cur.Ma_so.localeCompare(pre.Ma_so))
            return cur;
    }).Ma_so.substr(2);
    return Number(max);
}

export default movieListContainerReducer;
