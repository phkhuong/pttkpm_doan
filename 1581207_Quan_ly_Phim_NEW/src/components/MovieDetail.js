import React from "react";
import PropTypes from 'prop-types';
import { Link } from "react-router-dom";
import { Dia_chi_Media } from "../api";

import SessionList from "../components/SessionList";
import { Card, Icon, Avatar, Col, Row, Rate, Badge } from 'antd';
import 'antd/lib/rate/style/css';

class MovieDetail extends React.Component {
    constructor(props) {
        super(props);
    }


    render() {
        // console.log(this.props.movie);
        let { Ma_so, Ten, Noi_dung, Rating, Thoi_luong, Quoc_gia, The_loai, Dien_vien, Dao_dien, Nha_san_xuat, Khoi_chieu, Don_gia, Phan_loai, Doanh_thu, Danh_sach_Suat_chieu } = this.props.movie;
        if (Khoi_chieu != undefined) {
            Khoi_chieu = new Date(parseInt(Khoi_chieu.substr(6))).toLocaleDateString('vi-VN');
        }
        if(The_loai != undefined){
            The_loai = The_loai.join(', ');
        }
        let displayRate;
        if(Rating){
            displayRate = Math.round(Rating*2)/2;
        }
        const img_src = `${Dia_chi_Media}/${this.props.movie.Ma_so}.jpg`;
        const icons = [
            <Link to={`/movies/${Ma_so}`} >
                <Icon
                    style={{ fontSize: 20 }}
                    type="info-circle-o"
                />
            </Link>,
            <Icon type="form" style={{ fontSize: 20 }} />,
            <Icon type="delete" style={{ fontSize: 20 }} />
        ];
        return (
            <div>
                <Row>
                    <Col sm={24} md={12} lg={8}>
                        <Card
                            style={{ maxWidth: 300, marginBottom: 20 }}
                            cover={<img alt="example" src={img_src} />}
                            actions={icons}
                        >

                        </Card>
                    </Col>
                    <Col sm={24} md={12} lg={16}>
                        <h1>{Ten}</h1>
                        <Rate defaultValue={displayRate} value={displayRate} count={10} allowHalf /> {Rating}<br />
                        <div style={{fontSize:20}}>
                            <Badge style={{marginBottom: '0.3em'}} count={Phan_loai} /> <Icon type="play-circle-o" />{` ${Thoi_luong} phút`}<br />
                        </div>
                        <p style={{ lineHeigth: 2, fontSize: '1.5em' }}>
                            Quốc gia: {Quoc_gia}<br />
                            Thể loại: {The_loai}<br />
                            Diễn viên: {Dien_vien}<br />
                            Đạo diễn:  {Dao_dien}<br />
                            Nhà sản xuất:   {Nha_san_xuat}<br />
                            Ngày:  {Khoi_chieu}<br />
                            Đơn giá:  {Don_gia} đ<br />
                            Doanh thu: {Doanh_thu} đ<br />
                        </p>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h2>Nội dung phim</h2>
                        {Noi_dung}
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h2>Suất chiếu</h2>
                        {Danh_sach_Suat_chieu != undefined && <SessionList sessions={Danh_sach_Suat_chieu} />}
                    </Col>
                </Row>
            </div>
        );
    }
}


export default MovieDetail;