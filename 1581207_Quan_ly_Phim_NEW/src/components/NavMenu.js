import React, { Component } from 'react';
import { Route, Switch, Link, NavLink, withRouter } from 'react-router-dom';

// import { ConnectedRouter } from 'react-router-redux';


import { Layout, Menu, Breadcrumb } from 'antd';

const NavMenu = (props) => {
    let selectedKey = props.path;
    if (selectedKey.match(/^\/movies\/PHIM_(\d)+/)) {
        selectedKey = "/movies/:id";
    }

    return (
        <Menu
            theme="dark"
            mode="horizontal"
            defaultSelectedKeys={[selectedKey]}
            selectedKeys={[selectedKey]}
            style={{ lineHeight: '64px' }}
        >
            <Menu.Item key="/"><Link to="/">Home</Link></Menu.Item>
            <Menu.Item key="/about"><Link to="/about">About</Link></Menu.Item>
            <Menu.Item key="/movies"><Link to="/movies">Danh sách phim</Link ></Menu.Item>
            <Menu.Item key="/movies/:id">Chi tiết phim</Menu.Item>
            <Menu.Item key="6"><Link to="/add">Thêm phim mới</Link></Menu.Item>
        </Menu>
    );
}

export default NavMenu;