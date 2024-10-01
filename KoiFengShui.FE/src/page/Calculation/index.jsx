import "./index.css";
import koiImage from "./path-to-koi-image.jpg";
const Calculation = () => {
  return (
    <div className="login-container">
      <div className="login-image-container">
        <img src={koiImage} alt="Koi fish" className="koi-image" />
      </div>
      <div className="login-form-container">
        <h1>Feng Shui Consulting according to your Age</h1>
        <p>
          Hãy để chúng tôi giúp bạn cho loại cá và hỗ trợ theo năm sinh của bạn
        </p>
        <button className="button-see">Enter your YOB</button>
        <div className="year-selection-container">
          <input type="date"></input>
        </div>
        <footer>
          <button className="button-see">See now</button>
        </footer>
      </div>
    </div>
  );
};

export default Calculation;
