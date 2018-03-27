import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Switch, Route } from "react-router";

import { selectMovie, requestCinemas, deleteSession, createSession, requestUpdateMovie, resetApiResult, requestDeleteMovie } from '../MovieListContainer/actions';
import MovieDetail from "../../components/MovieDetail";
import MovieEdit from "../../components/MovieEdit";

/* eslint-disable react/prefer-stateless-function */
export class MovieDetailContainer extends React.Component {
    componentWillMount() {
        // request mv
        this.props.selectMovie(this.props.match.params.id);
        this.props.requestCinemas();
    }
    render() {
        // console.log(this.props);
        const movie = this.props.movie;
        return (
            <div>
                <Switch>
                    <Route path="/movies/:id" exact component={
                        (props) => (
                            <MovieDetail 
                                success={this.props.success}
                                error={this.props.error}
                                resetApiResult={this.props.resetApiResult} 
                                requestDeleteMovie={this.props.requestDeleteMovie} 
                                movie={movie} 
                            />)
                        } 
                    /> 
                    <Route 
                        path="/movies/:id/edit" 
                        component={
                            (props) => (
                                <MovieEdit 
                                    requestUpdateMovie={this.props.requestUpdateMovie}
                                    createSession={this.props.createSession}
                                    deleteSession={this.props.deleteSession} 
                                    movie={movie} 
                                    cinemas={this.props.cinemas}
                                    error={this.props.error}
                                    success={this.props.success}
                                    resetApiResult={this.props.resetApiResult}
                                    />
                                )}
                    />
                </Switch>
            </div>
        );
    }
}

function mapStateToProps(state) {
    // console.log(state);
    return {
        movies: state.MovieListContainerState.movies,
        movie: state.MovieListContainerState.selectedMovie,
        cinemas: state.MovieListContainerState.cinemas,
        error: state.MovieListContainerState.error,
        success: state.MovieListContainerState.success,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        requestCinemas: () => dispatch(requestCinemas()),
        selectMovie: (id) => dispatch(selectMovie(id)),
        deleteSession: (movieID, sessionID) => dispatch(deleteSession(movieID, sessionID)),
        createSession: (movieID, session) => dispatch(createSession(movieID, session)),
        requestUpdateMovie: (movie) => dispatch(requestUpdateMovie(movie)),
        resetApiResult: () => dispatch(resetApiResult()),
        requestDeleteMovie: (movieID) => dispatch(requestDeleteMovie(movieID)),
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(MovieDetailContainer);
