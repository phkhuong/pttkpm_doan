import React from "react";
import PropTypes from 'prop-types';
import { Row } from 'antd';

import Movie from "../components/Movie";

class MovieList extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const markup = this.props.movies.map((movie) => (
            <Movie key={movie.Ma_so} movie={movie}  />
        ));
        return (
            <div>
                <h1>Danh sách phim đang chiếu</h1>
                {this.props.movies.length}
                <Row>
                    {markup}
                </Row>
            </div>
        );
    }
}

MovieList.propTypes = {
    movies: PropTypes.array.isRequired,
}

export default MovieList;