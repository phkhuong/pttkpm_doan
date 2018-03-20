import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';

import { Switch, BrowserRouter as Router, Route, withRouter } from 'react-router-dom';
import reducer from './reducer';

import { requestMovies, selectMovie } from './actions';
// import { selectMovie } from "../MovieDetailContainer/actions";
import MovieList from "../../components/MovieList";
import MovieDetailContainer from "../MovieDetailContainer";
import About from "../../components/about";


/* eslint-disable react/prefer-stateless-function */
export class MovieListContainer extends React.Component {
    componentWillMount(){
        // request mv
        this.props.requestMovies();
    }
    render() {
        // console.log(this.props);
        const Movies = (props) => {
            return (
                <MovieList select={this.props.selectMovie} movies={this.props.movies} {...props} />
            );
        };
// console.log(this.props);
        const movie = this.props.selectedMovie;
        const getMovie = this.props.selectMovie;
        // const MoviePage = (props) => {
        //     return (
        //         <MovieDetail selectMovie={getMovie} movie={movie} {...props} />
        //     );
        // };
        return (
            <div>
                
                {
                    this.props.error !== '' &&
                        <h1 style={{ color: 'red' }}>{this.props.error}</h1> 
                        
                }
                
                <Switch>
                    <Route path="/movies" exact component={Movies} />
                    <Route path="/movies/:id" component={MovieDetailContainer} />
                </Switch>
            </div>
        );
    }
}

function mapStateToProps(state) {
    // console.log(state);
    return {
        movies: state.MovieListContainerState.movies,
        error: state.MovieListContainerState.error,
        selectedMovie: state.MovieListContainerState.movie,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        requestMovies: () => dispatch(requestMovies()),
        selectMovie: (id) => dispatch(selectMovie(id)),
    };
}



// export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CounterContainer));
export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MovieListContainer));
