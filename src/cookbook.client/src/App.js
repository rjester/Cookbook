import Home from './pages/Home';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';

import React, { Component, Fragment} from 'react';

//class ResultRow extends React.Component {
//    render() {
//        const recipe = this.props.recipe;
//        return (
//            <div className="flex-item">
//                <img src="logo192.png" />
//                <h3>{recipe.title}</h3>
//                {recipe.description}
//                <br />{recipe.id}
//            </div>
//        );
//    }
//}

//class ResultTable extends React.Component {
//    componentDidMount() {
//        //fetch('api/recipes')
//        //    .then(res => res.json())
//        //    .then(
//        //        (result) => {
//        //            this.setState({
//        //                //isLoaded: true,
//        //                recipes: result
//        //            });
//        //        },
//        //        // Note: it's important to handle errors here
//        //        // instead of a catch() block so that we don't swallow
//        //        // exceptions from actual bugs in components.
//        //        (error) => {
//        //            this.setState({
//        //                //isLoaded: true,
//        //                error
//        //            });
//        //        }
//        //    )
//    }

//    render() {
//        const rows = [];

//        this.props.recipes.forEach((recipe) => {
//            rows.push(
//                <ResultRow
//                    recipe={recipe}
//                    key={recipe.id} />
//            );
//        });

//        return (
//            <div className="flex-container">
//                {rows}
//            </div>
//        );
//    }
//}

//class SearchBar extends React.Component {
//    state = {
//        searchCriteria: '',
//        recipes: []
//    };

//    constructor(props) {
//        super(props);

//        this.handleSumbit = this.handleSubmit.bind(this);
//    }

//    handleChamge(event) {
//        this.setState({ [event.target.name]: event.target.value });
//    }

//    handleSubmit(event) {
//        event.preventDefault();
//        alert('you submitted the form');
//        fetch('api/recipes/search/test')
//            .then(res => res.json())
//            .then(
//                (result) => {
//                    console.log(result);
//                    this.setState({
//                        //isLoaded: true,
//                        recipes: result
//                    });
//                },
//                // Note: it's important to handle errors here
//                // instead of a catch() block so that we don't swallow
//                // exceptions from actual bugs in components.
//                (error) => {
//                    this.setState({
//                        //isLoaded: true,
//                        error
//                    });
//                }
//            )
//    }

//    render() {
//        return (
//            <form onSubmit={this.handleSubmit}>
//                <input name="searchCriteria" type="text" placeholder="Search..."
//                    onChange={this.handleChange} />
//                <input type="submit" value="Submit"></input>
//            </form>
//        );
//    }
//}

//class FilterableResultTable extends React.Component {
//    render() {
//        return (
//            <div>
//                <SearchBar />
//                <ResultTable recipes={this.props.recipes} />
//            </div>
//        );
//    }
//}


//export default class App extends Component {
//    static displayName = App.name;

//    constructor(props) {
//        super(props);
//        this.state = { recipes: [], searchCriteria: '', loading: true };
//    }



//    componentDidMount() {
//        this.populateRecipes();
//    }

//    static renderRecipesTable(recipes) {
//        return (
//            <table className='table table-striped' aria-labelledby="tabelLabel">
//                <thead>
//                    <tr>
//                        <th>Title</th>
//                    </tr>
//                </thead>
//                <tbody>
//                    {recipes.map(recipe =>
//                        <tr key={recipe.id}>
//                            <td>{recipe.title}<br />{recipe.description}</td>
//                        </tr>
//                    )}
//                </tbody>
//            </table>
//        );
//    }

//    //static Button() {
//    //    const [counter, setCounter] = useState(0);
//    //    const handleClick = () => setCounter(counter + 1);
//    //    return <button onclick={handleClick}>
//    //        {counter}
//    //    </button>
//    //}

//    render() {
//        let contents = this.state.loading
//            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
//            : App.renderRecipesTable(this.state.recipes);

//        return (
//            <FilterableResultTable recipes={this.state.recipes} />
//            //<div>
//            //    <h1 id="tabelLabel" >All</h1>
//            //    <p>This component demonstrates fetching data from the server.</p>
//            //    {contents}
//            //</div>
//        );
//    }

//    async populateRecipes() {
//        const response = await fetch('api/recipes');
//        const data = await response.json();
//        this.setState({ recipes: data, loading: false });
//    }
//}


function App() {
    return (
        <Router>
            <div>
                <nav>
                    <ul>
                        <li>
                            <Link to="/">Home</Link>
                        </li>
                        <li>
                            <Link to="/about">About</Link>
                        </li>
                        <li>
                            <Link to="/users">Users</Link>
                        </li>
                    </ul>
                </nav>

                <Routes>
                    <Route path="/about" element={<About />} />
                    <Route path="/users" element={<Users />} />
                    <Route path="/" element={<Home />} />
                </Routes>
            </div>
        </Router>
    );
}

function About() {
    return <h2>About</h2>;
}

function Users() {
    return <h2>Users</h2>;
}

export default App;
