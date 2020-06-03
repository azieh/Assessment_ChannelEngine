import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    componentDidMount() {
        this.populateProductsData();
    }

    renderProductsTable = (products) => {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>EAN</th>
                        <th>Total quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.name}>
                            <td>{product.name}</td>
                            <td>{product.ean}</td>
                            <td>{product.quantity}</td>
                            <td>
                                <button className="btn btn-primary"
                                    onClick={async () => {await this.updateProductData(product)}}
                                    disabled={product.isUpdated}>
                                    Set stock value to 25
                            </button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderProductsTable(this.state.products);

        return (
            <div>
                <h1 id="tabelLabel" >Orders form channelengine</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateProductsData() {
        const response = await fetch('products/top5');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }

    async updateProductData(product) {
        product.isUpdated = true;
        await fetch(`products/updateStockTo25?merchantProductNo=${product.merchantProductNo}`, {
            method: 'POST'
        });
        product.isUpdated = false;
    }
}

export default FetchData