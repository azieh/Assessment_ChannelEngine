import React, { Component } from 'react';
import { Container } from 'reactstrap';
import FetchData from './FetchData'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <Container>
        <FetchData></FetchData>
        </Container>
      </div>
    );
  }
}
