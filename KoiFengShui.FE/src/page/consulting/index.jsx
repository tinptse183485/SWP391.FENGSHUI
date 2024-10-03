import "./index.css";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import { useLocation } from "react-router-dom"; // Import useLocation
import { useState } from "react"; // Import useState
import { Popover } from 'antd'; // Import Popover from antd

function Consulting() {
  const location = useLocation(); // Get location object
  const { koiData, koiQuantity, pondShape, pondDirection } = location.state || {
    koiData: [],
    koiQuantity: [],
    pondShape: [],
    pondDirection: [],
  }; // Extract  data

  const [selectedKoi, setSelectedKoi] = useState(null); // State for selected koi
  const [selectedPond, setSelectedPond] = useState(null); // State for selected pond
  const [showKoiDetails, setShowKoiDetails] = useState(false); // State for koi details visibility
  const [showPondDetails, setShowPondDetails] = useState(false); // State for pond details visibility

  const contentKoi = (koi) => (
    <div className="popup">
      <img src={`/koi_image/${koi.image}`} alt={koi.image} />
      <div>
        <h1>{koi.koiType}</h1>
        <h3>Brief introduction:</h3>
        <p>{koi.description}</p>
        <h3>SUITABLE Feng shui:</h3>
        <p>{koi.element}</p>
        <h3>The suitable number of koi fish:</h3>
        <p>{koiQuantity.description}</p>
      </div>
    </div>
  );

  const contentPond = (pond) => (
    <div className="pond-popup">
      <img src={`/pond/${pond.image}`} alt={pond.image} />
      <h1>{pond.shapeId}</h1>
      {pondDirection.length > 0 &&
        pondDirection.map((direction) => (
          
            <div key={direction.directionId}>
            <h2>description:</h2>
            <h3>{direction.description}</h3>
            <h2>Eight Mansions:</h2>
            <h3>{direction.eightMansions}</h3>
            <h2>hướng:</h2>
            <h3>{direction.directionId}</h3>
          </div>
          
          
        ))}
    </div>
  );

  const showModal = (koi) => {
    setSelectedKoi(koi); // Set selected koi
    setShowKoiDetails(true); // Show koi details
  };

  const showPondModal = (pond) => {
    setSelectedPond(pond); // Set selected pond
    setShowPondDetails(true); // Show pond details
  };

  const handleCancel = () => {
    setShowKoiDetails(false); // Hide koi details
    setShowPondDetails(false); // Hide pond details
  };

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
                <Popover content={contentKoi(koi)} title="Koi Details" trigger="click">
                  <div className="koi-card">
                    <div className="image">
                      <img src={`/koi_image/${koi.image}`} alt={koi.image} />
                    </div>
                    <h3>{koi.koiType}</h3>
                  </div>
                </Popover>
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
                <Popover content={contentPond(pond)} title="Pond Details" trigger="click">
                  <div className="koi-card">
                    <div className="image">
                      <img src={`/pond/${pond.image}`} alt={pond.image} />
                    </div>
                    <h3>{pond.shapeId}</h3>
                  </div>
                </Popover>
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
      {showKoiDetails && (
        <div className="popup">
          <img src={`/koi_image/${selectedKoi.image}`} alt={selectedKoi.image} />
          <h1>Koi Fish:</h1>
          <h2>{selectedKoi.koiType}</h2>
          <h3>Brief introduction:</h3>
          <p>{selectedKoi.description}</p>
          <h3>Suitable Feng shui:</h3>
          <p>{selectedKoi.element}</p>
          <h3>The suitable number of koi Fish:</h3>
          <p>{koiQuantity.description}</p>
          <button onClick={handleCancel}>Close</button> {/* Button to close details */}
        </div>
      )}
      {showPondDetails && (
        <div className="popup">
          <img src={`/pond/${selectedPond.image}`} alt={selectedPond.image} />
          <h1>Pond Shape:</h1>
          <h3>{selectedPond.shapeId}</h3>
          {pondDirection.length > 0 &&
            pondDirection.map((direction) => (
              <div key={direction.directionId}>
                <h2>description:</h2>
                <h3>{direction.description}</h3>
                <h2>Eight Mansions:</h2>
                <h3>{direction.eightMansions}</h3>
                <h2>hướng:</h2>
                <h3>{direction.directionId}</h3>
              </div>
            ))}
          <button onClick={handleCancel}>Close</button> {/* Button to close details */}
        </div>
      )}
    </div>
  );
}
export default Consulting;
