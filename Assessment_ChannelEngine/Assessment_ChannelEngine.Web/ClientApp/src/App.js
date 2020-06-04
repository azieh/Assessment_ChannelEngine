import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchTop5 } from './components/FetchTop5';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route path='/fetchTop5' component={FetchTop5} />
      </Layout>
    );
  }
}
