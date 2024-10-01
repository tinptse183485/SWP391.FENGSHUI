import HeaderTemplate from "../../components/header-page";
import koiImage from "./path-to-koi-image.jpg";
import Koi2 from "./Koi2.jpg";
import "./index.css";
import FooterPage from "../../components/footer-page";
import { Carousel } from "antd";
import { Link } from "react-router-dom";
const contentStyle = {
  height: "500px",
  color: "#fff",
  lineHeight: "500px",
  textAlign: "center",
  background: "#364d79",
};
function Home() {
  return (
    <>
      <HeaderTemplate></HeaderTemplate>
      <Carousel autoplay className="carousel">
        <div>
          <img
            style={contentStyle}
            src={koiImage}
            alt="header-img"
            className="carousel-img"
          />
        </div>
        <div>
          <img
            style={contentStyle}
            src="https://images.unsplash.com/photo-1617908833148-6f81dc10d881?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8S29pJTIwZmlzaHxlbnwwfDB8MHx8fDA%3D"
            alt="header-img"
            className="carousel-img"
          />
        </div>
      </Carousel>
      <body>
      
        {/* Trending Feature */}
        <div className="container">
          <h2>Trending Feature</h2>
          <div className="Card-container">
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Pond  according to your Feng Shui element"
                className="img-feature"
              ></img>
              <h3>Selecting Pond according to your Feng Shui element</h3>
              <button className="button">Button</button>
            </div>
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Koi fish according to your Feng Shui element"
              ></img>
              <h3>Selecting Koi fish according to your Feng Shui element</h3>
              <button className="button">Button</button>
            </div>
            <div className="Card">
              <img src={koiImage} alt="Post Advertisement Function"></img>
              <h3>Post Advertisement Function</h3>
              <button className="button">Button</button>
            </div>
            <div className="Card">
              <img src={koiImage} alt="Calculate compatibility"></img>
              <h3>Calculate compatibility</h3>
              <button className="button">
                <Link to="/compute">Button</Link>
              </button>
            </div>
          </div>
        </div>
        {/* Blog */}
        <div className="container">
          <h2>Blog</h2>
          <div className="Card-container">
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Pond according to your Feng Shui element"
                className="img-feature"
              ></img>
              <h3>Selecting Pond according to your Feng Shui element</h3>
              <button className="button">Button</button>
            </div>
            <div className="Card">
              <img
                src={koiImage}
                alt="Selecting Koi fish according to your Feng Shui element"
              ></img>
              <h3>Selecting Koi fish according to your Feng Shui element</h3>
              <button className="button">Button</button>
            </div>
            <div className="Card">
              <img src={koiImage} alt="Post Advertisement Function"></img>
              <h3>Post Advertisement Function</h3>
              <button className="button">Button</button>
            </div>
          </div>
        </div>
      </body>
      <FooterPage></FooterPage>
    </>
  );
}

export default Home;
