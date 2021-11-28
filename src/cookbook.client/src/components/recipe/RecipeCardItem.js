import { Link } from 'react-router-dom';
import { useContext } from 'react';

const RecipeCardItem = ({ meal }) => {
    console.log(meal);
    return (
      <div
        key={meal.id}
      >
        {/* <Link to={`/MealInfo/${meal.idMeal}`}> */}
          <img
            // src={meal.strMealThumb}
            src='https://via.placeholder.com/300.png' 
            alt="stew"
            className="h-40 sm:h-40 w-full object-cover hover:opacity-75 transition-opacity duration-200 ease-in"
          />
          <div className="m-4">
            <div className="font-bold">{meal.title}</div>
            <div className="block text-sm">{meal.description}</div>
          </div>
        {/* </Link> */}
  
        {/* <button
                        className="home-btn absolute top-1 left-1 sm:top-0 am:left-2 hover:bg-white  py-2 px-2 rounded-sm"
                        style={{ background: bg, color: syntax }}
                      >
                        <BookmarkIcon className="home-icon h-5 w-5  hover:text-black" />
                      </button> */}
      </div>
    );
  };
  
  export default RecipeCardItem;

