import { 
    REQUEST_MOVIES, 
    REQUEST_MOVIES_SUCCESS, 
    REQUEST_MOVIES_FAIL, 
    SELECT_MOVIE,
    SELECT_MOVIE_SUCCESS,
    USER_LOGIN_REQUEST,
    USER_LOGIN_SUCCESS,
    USER_LOGIN_FAIL,
} from './constants';
import 'whatwg-fetch';
import { Dia_chi_Get_Danh_sach_Phim, Dia_chi_Dang_nhap } from "../../api";

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
    return function (dispatch) {
        return fetchMovies().then(
            function (data) {
                // console.log(data);
                return dispatch(requestMoviesSuccess(data));
            },
            function (err) {
                return dispatch(requestMoviesFail(err.message));
            }
        )
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
            const movie = getState().movies.find(m => m.Ma_so === id);
            dispatch(selectMovieSuccess(movie));
        }
    }
}
function shouldFetchMovies(state, id) {
    if (state.MovieListContainerState.movies){
        return true;
    }
    console.log(state);
    const movie = state.MovieListContainerState.movies.find(m => m.Ma_so == id);
    if (!movie) {
        return true
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