import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Switch, Link, NavLink, withRouter } from 'react-router-dom';

// import { ConnectedRouter } from 'react-router-redux';


import CounterContainer from './containers/CounterContainer';
import MovieListContainer from './containers/MovieListContainer';
import MovieDetailContainer from './containers/MovieDetailContainer';
import Home from './components/home';
import About from './components/about';
import NavMenu from "./components/NavMenu";

import { Layout, Menu, Breadcrumb } from 'antd';
import 'antd/dist/antd.css';
import Counter from './components/Counter';

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
            <NavMenu path={this.state.path} />
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
                <Route path='/about' component={About} />
                <Route path='/counter' component={CounterContainer} />
                {/* <Route path='/movies/:id' component={MovieDetailContainer} /> */}
                <Route path='/movies' component={MovieListContainer} />
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

export default withRouter(App);
