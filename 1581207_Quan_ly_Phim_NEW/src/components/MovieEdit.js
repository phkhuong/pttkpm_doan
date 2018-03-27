import React from 'react';
import * as moment from "moment";

import EditForm from "./EditForm";
import AddForm from "./AddForm";

class MovieEdit extends React.Component {
    state = {
        fields: {
            Ma_so: {
                value: this.props.movie.Ma_so,
            },
            Ten: {
                value: this.props.movie.Ten,
            },
            Noi_dung: {
                value: this.props.movie.Noi_dung,
            },
            Rating: {
                value: this.props.movie.Rating,
            },
            Thoi_luong: {
                value: this.props.movie.Thoi_luong,
            },
            Quoc_gia: {
                value: this.props.movie.Quoc_gia,
            },
            The_loai: {
                value: this.props.movie.The_loai ? this.props.movie.The_loai.join(', ') : '',
            },
            Dien_vien: {
                value: this.props.movie.Dien_vien,
            },
            Dao_dien: {
                value: this.props.movie.Dao_dien,
            },
            Nha_san_xuat: {
                value: this.props.movie.Nha_san_xuat,
            },
            Khoi_chieu: {
                value: moment(this.props.movie.Khoi_chieu),
            },
            Don_gia: {
                value: this.props.movie.Don_gia,
            },
            Phan_loai: {
                value: this.props.movie.Phan_loai,
            },
            Doanh_thu: {
                value: this.props.movie.Doanh_thu,
            },
            Danh_sach_Suat_chieu: {
                value: this.props.movie.Danh_sach_Suat_chieu,
            },
        },
    };    
    handleFormChange = (changedFields) => {
        this.setState(({ fields }) => ({
            fields: { ...fields, ...changedFields },
        }));
    }
    render(){

        return (
            <div>
                <h1>Cập nhật Phim</h1>
                <AddForm  
                    {...this.state.fields}
                    onChange={this.handleFormChange}
                    requestUpdateMovie={this.props.requestUpdateMovie}
                    createSession={this.props.createSession} 
                    deleteSession={this.props.deleteSession} 
                    // movie={this.props.movie} 
                    cinemas={this.props.cinemas}
                    error={this.props.error}
                    success={this.props.success}
                    resetApiResult={this.props.resetApiResult}
                />
            </div>
        );
    }
}

export default MovieEdit;