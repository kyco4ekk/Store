CREATE TABLE product (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(200) NOT NULL
);

CREATE TABLE incoming (
    id INT IDENTITY(1,1) PRIMARY KEY,
    date DATETIME2 NOT NULL,
    productId INT NOT NULL,
    amount INT NOT NULL,
    FOREIGN KEY (productId) REFERENCES product(id)
);

CREATE TABLE outgoing (
    id INT IDENTITY(1,1) PRIMARY KEY,
    date DATETIME2 NOT NULL,
    productId INT NOT NULL,
    amount INT NOT NULL,
    FOREIGN KEY (productId) REFERENCES product(id)
);
