import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';

// import { withRouter } from 'react-router-dom';

import { selectMovie } from '../MovieListContainer/actions';
import MovieDetail from "../../components/MovieDetail";


/* eslint-disable react/prefer-stateless-function */
export class MovieDetailContainer extends React.Component {
    componentWillMount() {
        // request mv
        this.props.selectMovie(this.props.match.params.id);
    }
    render() {
        // console.log(this.props);
        return (
            <div>
                
                <MovieDetail movie={this.props.movie} />

            </div>
        );
    }
}

function mapStateToProps(state) {
    // console.log(state);
    return {
        movie: state.MovieListContainerState.selectedMovie,
        // error: state.MovieDetailContainerState.error,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        selectMovie: (id) => dispatch(selectMovie(id)),

    };
}

export default connect(mapStateToProps, mapDispatchToProps)(MovieDetailContainer);
