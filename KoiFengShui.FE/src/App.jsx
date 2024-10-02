import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";
import Dashboard from "./components/dashboard";
import Calculation from "./page/calculation";
import Consulting from "./page/Consulting";
const App = () => {
  const router = createBrowserRouter([
    {
      path: "ss",
      element: <Home />,
    },
    {
      path: "login",
      element: <Login />,
    },
    {
      path: "register",
      element: <Register />,
    },
    {
      path: "dashboard",
      element: <Dashboard />,
    },
    {
      path: "calculation",
      element: <Calculation />,
    },
    {
      path: "/",
      element: <Consulting />,
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
