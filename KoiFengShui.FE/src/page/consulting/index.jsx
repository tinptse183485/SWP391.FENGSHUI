import "./index.css";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import { Link, useLocation } from "react-router-dom"; // Import useLocation
function Consulting() {
  const location = useLocation(); // Get location object
  const { koiData, koiQuantity, pondShape, pondDirection } = location.state || {
    koiData: [],
    koiQuantity: [],
    pondShape: [],
    pondDirection: [],
  }; // Extract  data

  return (
    <div>
      <header>
        <HeaderTemplate />
      </header>
      <body>
        <div className="Header-fish">
          <h2>Các loại cá phù hợp</h2>
          <div className="koi-cards">
            {koiData.length > 0 ? (
              koiData.map((koi) => (
                // eslint-disable-next-line react/jsx-key
                <div className="koi-card">
                  <img src={koi.image} />
                  <h3>{koi.koiType}</h3>
                </div>
              ))
            ) : (
              <p>No Koi data available</p>
            )}
          </div>
        </div>
        <div className="Header-pond">
          <h2>Đặc điểm hồ phù hợp</h2>
          <h3>Hình dạng:</h3>
          <div className="koi-pond">
            {pondShape.length > 0 ? (
              pondShape.map((pond) => (
                // eslint-disable-next-line react/jsx-key
                <div className="koi-card">
                  <img src={pond.image} />
                  <h3>{pond.shapeId}</h3>
                </div>
              ))
            ) : (
              <p>No pond data available</p>
            )}
          </div>
          <h3>Hướng:</h3>
          <div className="koi-pond">
            {pondDirection.length > 0 ? (
              pondDirection.map((pond) => (
                // eslint-disable-next-line react/jsx-key
                <div className="koi-card">
                  <h3>{pond.directionId}</h3>
                  <h3>{pond.eightMansions}</h3>
                  <h3>{pond.description}</h3>
                </div>
              ))
            ) : (
              <p>No pond data available</p>
            )}
          </div>
        </div>
      </body>
      <footer>
        <FooterPage />
      </footer>
    </div>
  );
}
export default Consulting;
