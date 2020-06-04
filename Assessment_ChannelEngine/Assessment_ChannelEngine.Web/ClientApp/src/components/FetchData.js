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
            <div>
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
                                    {!product.isUpdated ?
                                        <button className="btn btn-primary"
                                            onClick={async () => { await this.updateProductData(product) }}>
                                            Set stock value to 25</button>
                                        :
                                        <div className="spinner-border text-primary" role="status">
                                            <span className="sr-only">Loading...</span>
                                        </div>
                                    }
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <button className="btn btn-primary"
                    onClick={async () => { await this.populateProductsData() }}>
                    Update
                            </button>
            </div>
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
        this.setState({ products: [], loading: true });
        const response = await fetch('products/top5');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }

    async updateProductData(product) {
        this.setStockValueButtonLoading(product.merchantProductNo, true);
        await fetch(`products/updateStockTo25?merchantProductNo=${product.merchantProductNo}`, {
            method: 'POST'
        });
        this.setStockValueButtonLoading(product.merchantProductNo, false);
    }

    setStockValueButtonLoading = (merchantProductNo, isLoading) => {
        this.setState(prevState => ({
            products: prevState.products.map(item => (
                merchantProductNo === item.merchantProductNo ? { ...item, isUpdated: isLoading } : item
            ))
        }));
    }
}

export default FetchData