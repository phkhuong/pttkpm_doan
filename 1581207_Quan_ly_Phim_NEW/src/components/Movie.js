import React from "react";
import PropTypes from 'prop-types';
import { Link } from "react-router-dom";
import { Dia_chi_Media } from "../api";

import { Card, Icon, Avatar, Col, Row, Button } from 'antd';
const { Meta } = Card;


class Movie extends React.Component {
    constructor(props) {
        super(props);
    }
    
    render() {
        // console.log(this.props.movie);
        const { Ma_so, Ten, Noi_dung } = this.props.movie;
        const img_src = `${Dia_chi_Media}/${this.props.movie.Ma_so}.jpg`;
        const icons = [
            <Link to={`/movies/${Ma_so}`} >
                <Icon 
                    style={{ fontSize: 20 }} 
                    type="info-circle-o" 
                />
            </Link>, 
            <Link to={`/movies/${Ma_so}/edit`} >
                <Icon 
                    style={{ fontSize: 20 }} 
                    type="form" 
                />
            </Link>, 
            <Icon 
                type="delete" 
                style={{ fontSize: 20 }} 
                onClick={() => this.props.requestDeleteMovie(Ma_so)}
            />
        ];
        return (
            <Col sm={24} md={12} lg={8}>
                <Card
                    style={{ width: 300, marginBottom: 20 }}
                    cover={<Link to={`/movies/${Ma_so}`}><img alt="example" src={img_src} /></Link>}
                    actions={icons}
                    
                >
                    <Meta
                        
                        title={Ten}
                        
                    />
                </Card>
            </Col>
        );
    }
}

// Movie.propTypes = {
//     movie: PropTypes.object.isRequired,
// };

export default Movie;