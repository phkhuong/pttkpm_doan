import { SELECT_MOVIE } from "./constants";

export function selectMovie(movie) {
    return {
        type: SELECT_MOVIE,
        movie,
    }
}