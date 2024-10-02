import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";
import Dashboard from "./components/dashboard";
import ComputeCompability from "./page/calculate-compability";
const App = () => {
  const router = createBrowserRouter([
    {
      path: "/",
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
      path: "compute",
      element: <ComputeCompability />,
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
