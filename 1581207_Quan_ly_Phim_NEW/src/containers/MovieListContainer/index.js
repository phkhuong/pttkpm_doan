import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Switch, BrowserRouter as Router, Route, withRouter } from 'react-router-dom';
import { Input } from "antd";

import reducer from './reducer';

import { requestMovies, selectMovie, requestCinemas, resetApiResult, requestDeleteMovie } from './actions';
// import { selectMovie } from "../MovieDetailContainer/actions";
import MovieList from "../../components/MovieList";
import MovieDetailContainer from "../MovieDetailContainer";
import About from "../../components/about";

const Search = Input.Search;

/* eslint-disable react/prefer-stateless-function */
export class MovieListContainer extends React.Component {
    state = {
        keyword: ''
    }
    componentWillMount() {
        // request mv
        this.props.requestMovies();
    }
    componentDidMount() {
        // this.props.requestCinemas();
    }
    handleSearch(value) {
        this.setState({
            keyword: value
        });
    }
    search(){
        const keyword = this.state.keyword.toLowerCase();
        return this.props.movies.filter(m => {
            // search by ID
            const Ma_so = m.Ma_so.toLowerCase();
            // const id = Ma_so.substr(5);
            if(Ma_so.includes(keyword))
                return true;

            // searchByName
            const Ten = m.Ten.toLowerCase();
            if (Ten.includes(keyword))
                return true;

            // searchByActor
            const actors = m.Dien_vien.split(',');
            for(var i=0; i<actors.length; i++){
                if(actors[i].toLowerCase().includes(keyword)){
                    return true;
                }
            }
        });
    }
    searchByName(){
        const keyword = this.state.keyword.toLowerCase();
        return this.props.movies.filter(m => {
            const Ten = m.Ten.toLowerCase();
            return Ten.includes(keyword);
        });
    }

    render() {
        const movie = this.props.selectedMovie;
        const getMovie = this.props.selectMovie;
        
        const filteredMovies = this.search();
        return (
            <div>
                <Search
                    placeholder="Tìm tên phim, diễn viên"
                    onSearch={(value) => this.handleSearch(value)}
                    style={{ width: 400 }}
                    enterButton
                />
                {
                    this.props.error !== '' ?
                        <h1 style={{ color: 'red' }}>{this.props.error}</h1> :
                        <MovieList
                            select={this.props.selectMovie}
                            movies={filteredMovies}
                            error={this.props.error}
                            success={this.props.success}
                            resetApiResult={this.props.resetApiResult}
                            requestDeleteMovie={this.props.requestDeleteMovie}
                        />

                }

            </div>
        );
    }
}

function mapStateToProps(state) {
    // console.log(state);
    return {
        movies: state.MovieListContainerState.movies,
        cinemas: state.MovieListContainerState.cinemas,
        error: state.MovieListContainerState.error,
        success: state.MovieListContainerState.success,
        selectedMovie: state.MovieListContainerState.movie,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        requestMovies: () => dispatch(requestMovies()),
        requestCinemas: () => dispatch(requestCinemas()),
        selectMovie: (id) => dispatch(selectMovie(id)),
        resetApiResult: () => dispatch(resetApiResult()),
        requestDeleteMovie: (movieID) => dispatch(requestDeleteMovie(movieID)),
    };
}



// export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CounterContainer));
export default withRouter(connect(mapStateToProps, mapDispatchToProps)(MovieListContainer));
