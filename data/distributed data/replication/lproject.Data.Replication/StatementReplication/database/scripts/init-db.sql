CREATE TABLE IF NOT EXISTS orders (
      id UUID PRIMARY KEY,
      customer_email TEXT NOT NULL,
      total NUMERIC(12,2) NOT NULL,
      status TEXT NOT NULL,
      created_at TIMESTAMPTZ NOT NULL DEFAULT now()
);