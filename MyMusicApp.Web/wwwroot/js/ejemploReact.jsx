// const { extend } = require("jquery");


const data = [
    { nombreProducto = "Guitarra", precioProducto: 2500 },
    { nombreProducto = "Bateria", precioProducto: 7800 },
]

class Producto extends React.Component {
    render() {
        return (
            <div className="producto">
                <h2 className="nombreProducto">{this.props.NombreProducto}</h2>
                <h3 className="precioProducto">{this.props.PrecioProducto}</h3>
            </div>
        );
    }
}

class ListaProductos extends React.Component {
    render() {
        const fuenteProductos = data.map((d) =>
            <Producto NombreProducto={d.nombreProducto} PrecioProducto={d.precioProducto}></Producto>
        );


        return (
            <div className="listaProductos">
                { fuenteProductos}
            </div>
        );
    }
}

class ContenedorProducto extends React.Component {
    render() {
        return (
            <div className="contenedorProducto">
                <h1>Contenedor de productos</h1>
                <ListaProductos />
            </div>
        );
    }
}


ReactDOM.render(<ContenedorProducto />, document.getElementById('content'))





