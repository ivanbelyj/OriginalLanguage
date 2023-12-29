import { Link } from "react-router-dom";

export function Navigation() {
  return (
    <nav>
      <span>Navigation</span>
      <span>
        <Link to="/">Main</Link>
        <Link to="/about">About</Link>
        <Link to="/courses">Courses</Link>
        <Link to="/languages">Languages</Link>
      </span>
    </nav>
  );
}
