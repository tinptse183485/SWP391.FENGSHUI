import "./index.css";
import koiImage from "./path-to-koi-image.jpg";
function AuthenTemplate({ children }) {
  return (
    <div className="authen-template">
      <div className="login-image-container">
        <img src={koiImage} alt="Koi fish" className="koi-image" />
      </div>
      <div className="authen-template__form">{children}</div>
    </div>
  );
}

export default AuthenTemplate;
