import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Switch, Route } from "react-router";

import { selectMovie, deleteSession, createSession, requestUpdateMovie } from '../MovieListContainer/actions';
import MovieDetail from "../../components/MovieDetail";
import MovieEdit from "../../components/MovieEdit";

/* eslint-disable react/prefer-stateless-function */
export class MovieDetailContainer extends React.Component {
    componentWillMount() {
        // request mv
        this.props.selectMovie(this.props.match.params.id);
    }
    render() {
        // console.log(this.props);
        const movie = this.props.movie;
        return (
            <div>
                <Switch>
                    <Route path="/movies/:id" exact component={(props) => <MovieDetail movie={movie} />} /> 
                    <Route 
                        path="/movies/:id/edit" 
                        component={
                            (props) => (
                                <MovieEdit 
                                    requestUpdateMovie={this.props.requestUpdateMovie}
                                    createSession={this.props.createSession}
                                    deleteSession={this.props.deleteSession} 
                                    movie={movie} 
                                    cinemas={this.props.cinemas}/>
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
        // error: state.MovieDetailContainerState.error,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        selectMovie: (id) => dispatch(selectMovie(id)),
        deleteSession: (movieID, sessionID) => dispatch(deleteSession(movieID, sessionID)),
        createSession: (movieID, session) => dispatch(createSession(movieID, session)),
        requestUpdateMovie: (movie) => dispatch(requestUpdateMovie(movie)),
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(MovieDetailContainer);
