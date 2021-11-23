import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { recipes: [], loading: true };
    }

    componentDidMount() {
        this.populateRecipes();
    }

    static renderRecipesTable(recipes) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                    </tr>
                </thead>
                <tbody>
                    {recipes.map(recipe =>
                        <tr key={recipe.id}>
                            <td>{recipe.title}<br />{recipe.description}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderRecipesTable(this.state.recipes);

        return (
            <div>
                <h1 id="tabelLabel" >All</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateRecipes() {
        const response = await fetch('api/recipes');
        const data = await response.json();
        this.setState({ recipes: data, loading: false });
    }
}
