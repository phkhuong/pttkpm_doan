import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Switch, Link, NavLink, withRouter } from 'react-router-dom';
import { connect } from "react-redux";


import CounterContainer from './containers/CounterContainer';
import MovieListContainer from './containers/MovieListContainer';
import MovieDetailContainer from './containers/MovieDetailContainer';
import AddMovieContainer from "./containers/AddMovieContainer";
import LoginContainer from "./containers/LoginContainer";
import Home from './components/home';
import NavMenu from "./components/NavMenu";
import PrivateRoute from "./components/PrivateRoute";

import { Layout, Menu, Breadcrumb } from 'antd';
import 'antd/dist/antd.css';

const { Header, Content, Footer } = Layout;


class App extends Component {
  state = {
    path: this.props.location.pathname
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.location.pathname !== this.state.path)
      this.setState({ path: nextProps.location.pathname });
  }

  render() {

    return (
      <div>
        <Layout className="layout">
          <Header>
            <div className="logo" />
            <NavMenu path={this.state.path} user={this.props.user} />
          </Header>
          <Content style={{ padding: '0 20px' }}>
            <Breadcrumb style={{ margin: '16px 0' }}>
              <Breadcrumb.Item>Home</Breadcrumb.Item>
              <Breadcrumb.Item>List</Breadcrumb.Item>
              <Breadcrumb.Item>App</Breadcrumb.Item>
            </Breadcrumb>
            <div style={{ background: '#fff', padding: 24, minHeight: 700 }}>

              <Switch>
                <Route path='/' exact component={Home} />
                <Route path='/login' component={LoginContainer} />
                <PrivateRoute path='/add' isLoggedIn={this.props.isLoggedIn} component={AddMovieContainer} />
                <PrivateRoute path='/movies' exact isLoggedIn={this.props.isLoggedIn} component={MovieListContainer} />
                <PrivateRoute path='/movies/:id' isLoggedIn={this.props.isLoggedIn} component={MovieDetailContainer} />
              </Switch>
            </div>
          </Content>
          <Footer style={{ textAlign: 'center' }}>
            Duy Tu ©2018
          </Footer>
        </Layout>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    user: state.MovieListContainerState.user,
    isLoggedIn: state.MovieListContainerState.isLoggedIn,
  }
}

export default withRouter(connect(mapStateToProps)(App));
