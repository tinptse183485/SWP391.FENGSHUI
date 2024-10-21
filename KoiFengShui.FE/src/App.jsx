import {
  RouterProvider,
  createBrowserRouter,
  Navigate,
  useLocation,
} from "react-router-dom";
import { toast } from "react-toastify";
import Home from "./page/home";
import Login from "./page/login";
import Register from "./page/register";

import Calculation from "./page/calculation";
import Consulting from "./page/consulting";
import CalculateCompability from "./page/calculate-compability";
import User from "./page/Admin-page/User";
import ADS from "./page/Admin-page/Ads";
import Blog from "./page/Admin-page/blog";
import Koi from "./page/Admin-page/koi";
import Pond from "./page/Admin-page/pond";
import Ads_list from "./page/Ads_list";
import CreateAds from "./page/CreateAds";
import User_Ads from "./page/User_Ads";

import Dashboard from "./components/dashboard";

import AdvertisementDetail from "./page/AdvertismentDetail";
import PaymentSuccess from "./page/PaymentSuccess";
import VNPayPayment from "./page/Payment";
import ChoosePackage from "./page/ChoosePackage";
import CreateBlog from "./page/Admin-page/CreateBlog";
import BlogDetail from "./page/Admin-page/BlogDetail";
import BlogList from "./page/Blogs-list";
import AdminDashboard from "./page/Admin-page/dashboard";
import UserProfile from "./page/User-Profile";
import ForgotPassword from "./page/Forgot-Password";
import ResetPassword from "./page/Reset-Password";

function App() {
  const ProtectedRoute = ({ children }) => {
    const location = useLocation();
    const userId = localStorage.getItem("userId");

    if (!userId) {
      toast.error("Vui lòng đăng nhập để truy cập trang này.");
      return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return children;
  };

  const ProtectedRouteAdmin = ({ children }) => {
    const userId = localStorage.getItem("userId");
    const userRole = localStorage.getItem("role");

    if (userId && userRole === "Admin") {
      return children;
    }
    toast.error("Vui lòng đăng nhập để truy cập trang này.");
    return <Navigate to="/login" replace />;
  };
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
      path: "forgot-password",
      element: <ForgotPassword />,
    },
    {
      path: "reset-password/:email",
      element: <ResetPassword />,
    },
    {
      path: "dashboard",
      element: (
        <ProtectedRouteAdmin>
          <Dashboard />
        </ProtectedRouteAdmin>
      ),
      children: [
        {
          path: "AdminDashboard",
          element: <AdminDashboard />,
        },
        {
          path: "user",
          element: <User />,
        },

        {
          path: "ads",
          element: <ADS />,
        },
        {
          path: "blog",
          element: <Blog />,
        },
        {
          path: "create-blog",
          element: <CreateBlog />,
        },
        {
          path: "koi",
          element: <Koi />,
        },
        {
          path: "pond",
          element: <Pond />,
        },
      ],
    },

    {
      path: "calculation",
      element: <Calculation />,
    },
    {
      path: "consulting",
      element: <Consulting />,
    },
    {
      path: "calculate-compability",
      element: <CalculateCompability />,
    },

    {
      path: "blogs-list",
      element: <BlogList />,
    },
    {
      path: "ads-list",
      element: <Ads_list />,
    },
    {
      path: "user-profile",
      element: (
        <ProtectedRoute>
          <UserProfile />
        </ProtectedRoute>
      ),
    },
    {
      path: "create-ads",
      element: (
        <ProtectedRoute>
          <CreateAds />
        </ProtectedRoute>
      ),
    },
    {
      path: "user-ads",
      element: (
        <ProtectedRoute>
          <User_Ads />
        </ProtectedRoute>
      ),
    },

    {
      path: "advertisement-detail/:id",
      element: <AdvertisementDetail />,
    },
    {
      path: "payment",
      element: <VNPayPayment />,
    },
    {
      path: "payment-success",
      element: <PaymentSuccess />,
    },
    {
      path: "blog-detail/:id",
      element: <BlogDetail />,
    },
    {
      path: "/choose-package",
      element: <ChoosePackage />,
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
