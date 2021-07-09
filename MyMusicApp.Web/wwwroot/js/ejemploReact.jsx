const { extend } = require("jquery");


const data = [
    { nombreProducto = "Guitarra", precioProducto: 2500 },
    { nombreProducto = "Bateria", precioProducto: 7800 },
]

class Producto extend React.Component {
    render() {
        return (
            <div className="producto">
                <h2 className="nombreProducto"></h2>
                <h3 className="precioProducto"></h3>
            </div>
        );
    }
}

class ListaProductos extends React.Component {
    render() {
        return (
            const fuenteProds = this.props.data.map();

            <div className = "ListaProductos" >
                <Producto NombreProducto="Guitarra">
                </Producto> >
            </div>
            );
    }
}

class ContenerProducto extends React.Component {
    render() {
        return (
            <div className="contenedorProducto">
                <h1 class="alert alert-info">Productos</h1>
                <ListaProductos data= />
            </div>
        );
    }
}


ReactDOM.render(<ContenerProducto data={data} />, document.getElementById('content'));





