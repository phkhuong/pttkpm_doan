import { 
    REQUEST_MOVIES, 
    REQUEST_MOVIES_SUCCESS, 
    REQUEST_MOVIES_FAIL, 
    SELECT_MOVIE,
    SELECT_MOVIE_SUCCESS,
    USER_LOGIN_REQUEST,
    USER_LOGIN_SUCCESS,
    USER_LOGIN_FAIL,
    REQUEST_CINEMAS,
    REQUEST_CINEMAS_SUCCESS,
    REQUEST_CINEMAS_FAIL,
    DELETE_SESSION,
    CREATE_SESSION,
    REQUEST_UPDATE_MOVIE,
    UPDATE_MOVIE_SUCCESS,
    UPDATE_MOVIE_FAIL,
} from './constants';
import 'whatwg-fetch';
import { 
    Dia_chi_Get_Danh_sach_Phim, 
    Dia_chi_Dang_nhap,
    Dia_chi_Get_Danh_sach_Rap,
    Dia_chi_Cap_nhat_Phim,
} from "../../api";

function fetchMovies() {
    return fetch(Dia_chi_Get_Danh_sach_Phim)
        .then(checkStatus)
        .then(parseJSON);
    
    //fetch(Dia_chi_Get_Danh_sach_Phim)
        // .then(function (response) {
        //     return response.json()
        // }).catch(function (ex) {
        //     throw ex;
        // });
    
       
}
function fetchCinemas() {
    return fetch(Dia_chi_Get_Danh_sach_Rap)
        .then(checkStatus)
        .then(parseJSON);
}

function checkStatus(response) {
    if (response.status >= 200 && response.status < 300) {
        return response;
    } else {
        var error = new Error(`${response.status}: ${response.statusText}`);
        error.response = response;
        throw error;
    }
}

function parseJSON(response) {
    return response.json()
}

// async action, redux-thunk
export function requestMovies() {
    return (dispatch, getState) => {
        if (shouldFetchMovies(getState())) {
            // Dispatch a thunk from thunk!
            return fetchMovies().then(
                function (data) {
                    // console.log(data);
                    dispatch(requestMoviesSuccess(data));
                },
                function (err) {
                    return dispatch(requestMoviesFail(err.message));
                }
            );
        }
    }
}


export function requestMoviesSuccess(movies) {
    return {
        type: REQUEST_MOVIES_SUCCESS,
        movies,
    }
}
export function requestMoviesFail(error) {
    return {
        type: REQUEST_MOVIES_FAIL,
        error,
    }
}


export function requestCinemas() {
    return (dispatch, getState) => {
        if (shouldFetchCinemas(getState())) {
            // Dispatch a thunk from thunk!
            return fetchCinemas().then(
                function (data) {
                    dispatch(requestCinemasSuccess(data));
                },
                function (err) {
                    return dispatch(requestCinemasFail(err.message));
                }
            );
        }
    }
}
export function requestCinemasSuccess(cinemas) {
    return {
        type: REQUEST_CINEMAS_SUCCESS,
        cinemas,
    }
}
export function requestCinemasFail(error) {
    return {
        type: REQUEST_CINEMAS_FAIL,
        error,
    }
}


export function selectMovieSuccess(selectedMovie) {
    return {
        type: SELECT_MOVIE_SUCCESS,
        selectedMovie,
    }
}
export function selectMovie(id) {
    return (dispatch, getState) => {
        if (shouldFetchMovies(getState(), id)) {
            // Dispatch a thunk from thunk!
            return fetchMovies().then(
                function (data) {
                    dispatch(requestMoviesSuccess(data));
                    const movie = getState().MovieListContainerState.movies.find(m => m.Ma_so === id);
                    dispatch(selectMovieSuccess(movie));
                },
                function (err) {
                    return dispatch(requestMoviesFail(err.message));
                }
            );
        } else {
            // Let the calling code know there's nothing to wait for.
            const movie = getState().MovieListContainerState.movies.find(m => m.Ma_so === id);
            dispatch(selectMovieSuccess(movie));
        }
    }
}
function shouldFetchCinemas(state) {
    if (!state.MovieListContainerState.cinemas.length){
        return true;
    }
    return false;
}
function shouldFetchMovies(state, id = null) {
    if (!state.MovieListContainerState.movies.length){
        return true;
    }
    // console.log(state);
    if(id){
        const movie = state.MovieListContainerState.movies.find(m => m.Ma_so == id);
        if (!movie) {
            return true
        }
    }
    return false;
}

// send post request with username, password in body
function sendLoginRequest(user) {
    return fetch(Dia_chi_Dang_nhap,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(checkStatus)
        .then(parseJSON);
}
export function requestLogin(user) {
    return function (dispatch) {
        return sendLoginRequest(user)
            .then(
                function (data) {
                    // console.log(data);
                    return dispatch(requestLoginSuccess(data));
                },
                function (err) {
                    return dispatch(requestLoginFail(err.message));
                }
            )
    }
}

export function requestLoginSuccess(user) {
    return {
        type: USER_LOGIN_SUCCESS,
        user,
    };
}
export function requestLoginFail(error) {
    return {
        type: USER_LOGIN_FAIL,
        error,
    };
}

//////////////////////////////////////////////////
export function deleteSession(movieID, sessionID) {
    return {
        type: DELETE_SESSION,
        movieID,
        sessionID
    }
}
export function createSession(movieID, session) {
    return {
        type: CREATE_SESSION,
        movieID,
        session
    }
}

/////////////////////////////////////////////////
function sendUpdateMovie(movie) {
    return fetch(Dia_chi_Cap_nhat_Phim,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(movie)
    })
        .then(checkStatus)
        .then(parseJSON);
}
export function requestUpdateMovie(movie) {
    return function (dispatch) {
        return sendUpdateMovie(movie)
            .then(
                function (data) {
                    // console.log(data);
                    return dispatch(updateMovieSuccess(data.result));
                },
                function (err) {
                    return dispatch(updateMovieFail(err.message));
                }
            )
    }
}
export function updateMovieSuccess(movieName) {
    return {
        type: UPDATE_MOVIE_SUCCESS,
        message: `Cập nhật phim "${movieName}" thành công`,
    }
}
export function updateMovieFail(error) {
    return {
        type: UPDATE_MOVIE_FAIL,
        error,
    }
}