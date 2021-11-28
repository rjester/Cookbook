import * as FaIcons from 'react-icons/fa';

const SearchBox = ({ query, handleChange, handleSubmit }) => {
  return (
    <div>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={query}
          onChange={handleChange}
          placeholder="Search"
        />
        <button
            className="search-btn border rounded-r-full focus:outline-none"
            type="submit"
            // style={{ background: bg, color: syntax, borderColor: bg }}
          >
            <FaIcons.FaSearch className="h-5 w-5" />
          </button>
      </form>
    </div>
  );
};

export default SearchBox;
