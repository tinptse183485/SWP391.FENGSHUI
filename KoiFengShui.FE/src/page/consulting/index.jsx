import "./index.css";
import HeaderTemplate from "../../components/header-page";
import FooterPage from "../../components/footer-page";
import { useLocation } from "react-router-dom"; // Import useLocation
import { Modal } from "antd"; // Import Modal from Ant Design
import { useState } from "react"; // Import useState

function Consulting() {
  const location = useLocation(); // Get location object
  const { koiData, koiQuantity, pondShape, pondDirection } = location.state || {
    koiData: [],
    koiQuantity: [],
    pondShape: [],
    pondDirection: [],
  }; // Extract  data

  const [visible, setVisible] = useState(false); // State for modal visibility
  const [selectedKoi, setSelectedKoi] = useState(null); // State for selected koi
  const [selectedPond, setSelectedPond] = useState(null); // State for selected pond

  const showModal = (koi) => {
    setSelectedKoi(koi); // Set selected koi
    setVisible(true); // Show modal
  };

  const showPondModal = (pond) => {
    setSelectedPond(pond); // Set selected pond
    setVisible(true); // Show modal
  };

  const handleCancel = () => {
    setVisible(false); // Hide modal
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
                // eslint-disable-next-line react/jsx-key
                <div className="koi-card" onClick={() => showModal(koi)}>
                  {" "}
                  {/* Add onClick to show modal */}
                  <div className="image"> <img  src={`/koi_image/${koi.image}`} alt={koi.image} /></div>  
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
          
          <div className="pond-shape">
          <h3>Hình dạng: {pondShape.map((pond, index) => (
    <span key={pond.shapeId}>
      {index > 0 && " và "} {/* Add "và" between elements, except before the first one */}
      {pond.shapeId}
    </span>
  ))}</h3>

          
           <div className="pond-shape-images">
              {pondShape.map((pond) => (
                <div key={pond.shapeId} className="parallelogram" >
                  <img src={`/pond/${pond.image}`} alt={pond.shapeId} />
                  
                </div>
              ))}
            </div>
           </div>
          
         
          
           <div className="pond-direction">
  <h3>Hướng:</h3>
  {pondDirection.map((pond) => (
    <div key={pond.directionId} className="direction-info">
      <p>+ Gia chủ cung {pond.lifePalaceId} phù hợp với hướng {Array.isArray(pond.directionId) ? pond.directionId.join(' và ') : pond.directionId} 
           {" "}là hướng tốt {Array.isArray(pond.eightMansions) ? pond.eightMansions.join(' và ') : pond.eightMansions} theo Bát Trạch</p>
      {pond.pointOfDirection === 1 && (
        <p>+ {Array.isArray(pond.eightMansions) ? pond.eightMansions[0] : pond.eightMansions} (hướng Thượng Cát): Đây là hướng mang lại may mắn nhất 
           cho chủ nhà và tạo ra khí tốt.</p>
      )}
      {pond.pointOfDirection === 1 && Array.isArray(pond.eightMansions) && pond.eightMansions.length > 1 && (
        <p>+ {pond.eightMansions[1]} (hướng Thượng Cát): Đây là hướng mang lại may mắn nhất 
           cho chủ nhà và tạo ra khí tốt.</p>
      )}
    </div>
  ))}
</div>
        </div>
      </body>
      <footer>
        <FooterPage />
      </footer>
      {/* Modal for displaying koi details */}
      <Modal
        title={selectedKoi ? "": ""}
        visible={visible}
        onCancel={handleCancel}
        footer={null}
        width={1000}
      >
        {selectedKoi && (
          <div className="modal-content"> 
            <div className="modal-image"> 
               <img style={{width:"100%"}} src={`/koi_image/${selectedKoi.image}`}/>
            </div>
            
            <div className="modal-text">
            
              <h2>{selectedKoi.koiType}</h2>
              <h3>Giới thiệu:</h3>
              <p>{selectedKoi.description}</p>
              <h3>Mệnh cá:</h3>
              <p>{selectedKoi.element}</p>
              <h3>Số lượng :</h3>
              <p>{koiQuantity.description}</p>
            </div>
          </div>
          
        )}
      </Modal>
      {/* Modal for displaying pond details */}
      
    </div>
  );
}
export default Consulting;
