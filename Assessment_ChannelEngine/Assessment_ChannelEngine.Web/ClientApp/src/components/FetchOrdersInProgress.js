import React, { Component } from 'react';

export class FetchOrdersInProgress extends Component {
    static displayName = FetchOrdersInProgress.name;

    constructor(props) {
        super(props);
        this.state = { orders: [], loading: true };
    }

    componentDidMount() {
        this.populateOrdersData();
    }

    renderOrdersTable = (orders) => {
        return (
            <div>
                {orders.map(order =>
                    <div key={order.id}>
                        <p>Order Id: {order.id}</p>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>MerchantProductNo</th>
                                    <th>Quantity</th>
                                </tr>
                            </thead>
                            <tbody>
                                {order.lines.map((line, index) =>
                                    <tr key={index}>
                                        <td>{line.merchantProductNo}</td>
                                        <td>{line.quantity}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
                <button className="btn btn-primary"
                    onClick={async () => { await this.populateOrdersData() }}>
                    Update</button>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderOrdersTable(this.state.orders);

        return (
            <div>
                <h1 id="tabelLabel" >Orders form channelengine</h1>
                <p>This component demonstrates fetching orders from orders in status IN_PROGRESS</p>
                {contents}
            </div>
        );
    }

    async populateOrdersData() {
        this.setState({ orders: [], loading: true });
        const response = await fetch('orders/inProcess');
        const data = await response.json();
        this.setState({ orders: data, loading: false });
    }
}

export default FetchOrdersInProgress