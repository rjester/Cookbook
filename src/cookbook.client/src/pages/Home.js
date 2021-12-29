import { Fragment, useState } from "react";
import SearchBox from "../components/SearchBox/SearchBox";
import useCookbookApi from "../components/useCookbookApi";
import RecipeCardItem from '../components/recipe/RecipeCardItem';

const Home = () => {
  const [query, setQuery] = useState("");
  const [showResults, setShowResults] = useState(false);
  const [{ data, isLoading, isError }, doFetch] = useCookbookApi(
    `api/recipes/search/${query}`,
    { recipes: [] }
  );
  
  const currentRecipes = data ?? [];
  //const currentRecipes =  data.length() > 0 && data.slice(0, 5);

  console.log('currentRecipes');
  console.log(currentRecipes);


  const refresh = () => {
    // doFetch(`api/recipes/search/${query}`);
  };

  const handleChange = (event) => {
    setQuery(event.target.value);
  };

  const handleSubmit = (event) => {
    console.log('begin submit');
    //doFetch(`api/recipes/search/${query}`);
    event.preventDefault();
    setShowResults(true);
    console.log(showResults);
    console.log('end submit');
  };
console.log('[' + query + ']');
  return (
    <Fragment>
      <SearchBox
        query={query}
        handleChange={handleChange}
        handleSubmit={handleSubmit}
      />
<p>{showResults}</p>
 {showResults ? (
  
      <div
        id="recipes"
        className="meals transition-all duration-1000 ease-out"
      >
          {currentRecipes &&
                currentRecipes.map((meal) => (
                  <RecipeCardItem meal={meal} />
                ))}  
      </div>
 ) : (
    <p></p>   
  )}
    </Fragment>
  );
};

export default Home;
