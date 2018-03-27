import React from 'react';
// import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Switch, Route } from "react-router";
import * as moment from "moment";

import { requestAddMovie, requestCinemas, resetApiResult } from '../MovieListContainer/actions';
import AddForm from "../../components/AddForm";

/* eslint-disable react/prefer-stateless-function */
export class AddMovieContainer extends React.Component {
    state = {
        fields: {
            Ma_so: {
                value: 'PHIM_100',
            },
            Ten: {
                value: '',
            },
            Noi_dung: {
                value: '',
            },
            Rating: {
                value: 0,
            },
            Thoi_luong: {
                value: 100,
            },
            Quoc_gia: {
                value: '',
            },
            The_loai: {
                value: '',
            },
            Dien_vien: {
                value: '',
            },
            Dao_dien: {
                value: '',
            },
            Nha_san_xuat: {
                value: '',
            },
            Khoi_chieu: {
                value: moment(),
            },
            Don_gia: {
                value: 45000,
            },
            Phan_loai: {
                value: '',
            },
            Doanh_thu: {
                value: 0,
            },
            Danh_sach_Suat_chieu: {
                value: [],
            },
        },
    };
    componentDidMount() {
        this.props.requestCinemas();
    }
    componentWillUnmount() {
        if (this.props.error || this.props.success)
            this.props.resetApiResult();
    }
    handleFormChange = (changedFields) => {
        this.setState(({ fields }) => ({
            fields: { ...fields, ...changedFields },
        }));
    }
    render() {
        return (
            <div>
                <AddForm resetApiResult={this.props.resetApiResult} error={this.props.error} success={this.props.success} requestAddMovie={this.props.requestAddMovie} {...this.state.fields} onChange={this.handleFormChange} cinemas={this.props.cinemas} />


            </div>
        );
    }
}

function mapStateToProps(state) {
    // console.log(state);
    return {
        // movies: state.MovieListContainerState.movies,
        // movie: state.MovieListContainerState.selectedMovie,
        cinemas: state.MovieListContainerState.cinemas,
        error: state.MovieListContainerState.error,
        success: state.MovieListContainerState.success,
    };
}

function mapDispatchToProps(dispatch) {
    return {
        requestCinemas: () => dispatch(requestCinemas()),
        requestAddMovie: (movie) => dispatch(requestAddMovie(movie)),
        resetApiResult: () => dispatch(resetApiResult())
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(AddMovieContainer);
