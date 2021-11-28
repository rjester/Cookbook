import { Fragment, useState } from "react";
import SearchBox from "../components/SearchBox/SearchBox";
import useCookbookApi from "../components/useCookbookApi";
import RecipeCardItem from '../components/recipe/RecipeCardItem';

const Home = () => {
  const [query, setQuery] = useState("");
  const [{ data, isLoading, isError }, doFetch] = useCookbookApi(
    `api/recipes/search/${query}`,
    { recipes: [] }
  );

  console.log('data');
  console.log(data);

  const currentRecipes =  data && data.slice(0, 5);

  console.log('currentRecipes');
  console.log(currentRecipes);


  const refresh = () => {
    // doFetch(`api/recipes/search/${query}`);
  };

  const handleChange = (event) => {
    setQuery(event.target.value);
  };

  const handleSubmit = (event) => {
    doFetch(`api/recipes/search/${query}`);
    event.preventDefault();
  };

  return (
    <Fragment>
      <SearchBox
        query={query}
        handleChange={handleChange}
        handleSubmit={handleSubmit}
      />

      <div
        id="recipes"
        className="recipes transition-all duration-1000 ease-out"
      >
          {currentRecipes &&
                currentRecipes.map((meal) => (
                  <RecipeCardItem meal={meal} />
                ))}  
      </div>
    </Fragment>
  );
};

export default Home;
