import React, { Component } from 'react';
import { Container } from 'reactstrap';
import FetchOrdersInProgress from './FetchOrdersInProgress'
import FetchTop5 from './FetchTop5'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <Container>
        <FetchOrdersInProgress></FetchOrdersInProgress>
        <FetchTop5></FetchTop5>
        </Container>
      </div>
    );
  }
}
