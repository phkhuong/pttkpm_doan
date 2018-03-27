import React from "react";
import PropTypes from 'prop-types';
import { Row, message } from 'antd';

import Movie from "../components/Movie";

class MovieList extends React.Component {
    constructor(props) {
        super(props);
    }
    
    onSuccess(mess) {
        message.success(mess);
        this.props.resetApiResult();
    }
    onFail(mess) {
        message.error(mess);
        this.props.resetApiResult();
    }
    render() {
        const markup = this.props.movies.map((movie) => (
            <Movie key={movie.Ma_so} movie={movie} requestDeleteMovie={this.props.requestDeleteMovie} />
        ));
        return (
            <div>
                <h1>Danh sách phim đang chiếu</h1>
                {this.props.movies.length}
                <Row>
                    {markup}
                </Row>
                {
                    this.props.error && this.onFail(this.props.error)
                }
                {
                    this.props.success && this.onSuccess(this.props.success)
                }
            </div>
        );
    }
}

MovieList.propTypes = {
    movies: PropTypes.array.isRequired,
}

export default MovieList;