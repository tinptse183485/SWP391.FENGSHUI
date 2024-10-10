Feature('PackageAPI');
Scenario('Get All Packages', async ({ I }) => {
    const response = await I.sendGetRequest('/api/Package/GetAllPackage');
    
    I.seeResponseCodeIs(200);
    I.seeResponseContainsJson({
      "rank": "Diamond",
      "duration": 30,
      "description": "Hình ảnh lớn, xuất hiện ở đầu trang, tần suất xuất hiện nhiều, phù hợp với các doanh nghiệp lớn muốn quảng bá mạnh mẽ.",
      "price": 6000000,
      "adsPackages": []
    });
    
    I.seeResponseContainsJson({
      "rank": "Gold",
      "duration": 30,
      "description": "Hình ảnh vừa, xuất hiện ở dưới gói Dimond, tần suất xuất hiện vừa, phù hợp với doanh nghiệp vừa và nhỏ muốn có sự hiện diện ổn định.",
      "price": 4000000,
      "adsPackages": []
    });
    
    I.seeResponseContainsJson({
      "rank": "Silver",
      "duration": 30,
      "description": "Hình ảnh nhỏ, chỉ xuất hiện ở trang đăng tin, tần suất xuất hiện ít, phù hợp với doanh nghiệp cá nhân hoặc startup muốn tiết kiệm chi phí.",
      "price": 2000000,
      "adsPackages": []
    });
});


Scenario('Add Package', async ({ I }) => {
  const newPackage = {
    rank: "Test3",
    duration: 30,
    description: "Test Package",
    price: 100
  };
  
  const response = await I.sendPostRequest('/api/Package/AddPackage', newPackage);
  
  I.seeResponseCodeIs(200);
  I.seeResponseEquals("Tạo gói thành công");
});

Scenario('Update Package', async ({ I }) => {
  const updatedPackage = {
    Rank: "Test3",
    Duration: 60,
    Description: "Updated Test Package",
    Price: 150
  };
  
  const response = await I.sendPutRequest('/api/Package/UpdatePackage', updatedPackage);
  
  I.seeResponseCodeIs(200);
  I.seeResponseEquals('Cập nhật gói thành công');
});

Scenario('Delete Package', async ({ I }) => {
  const rank = "Test3"; 
  const response = await I.sendDeleteRequest(`/api/Package/DeletePackage/${rank}`);
  
  I.seeResponseCodeIs(200);
  I.seeResponseEquals('Package deleted successfully');
});