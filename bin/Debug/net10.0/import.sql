-- =========================================================
-- 1. INSERINDO INGREDIENTES (IDs de 1 a 15)
-- =========================================================
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Arroz Branco', 1);       -- 1
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Feijão Tropeiro', 1);    -- 2
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Carne de Sol', 1);       -- 3
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Peixe Fresco', 1);       -- 4
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Camarão Rosa', 1);       -- 5
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Peito de Frango', 1);    -- 6
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Massa Artesanal', 1);    -- 7
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Tomate Cereja', 1);      -- 8
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Queijo Coalho', 1);      -- 9
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Queijo Parmesão', 1);    -- 10
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Batata Rústica', 1);     -- 11
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Alface Americana', 1);   -- 12
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Azeite Trufado', 1);     -- 13
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Cebola Roxa', 1);        -- 14
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES ('Filé Mignon', 1);        -- 15

-- =========================================================
-- 2. INSERINDO PRATOS DE ALMOÇO (Período = 0) (IDs de 1 a 10)
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Rubacão Tocantinense', 'Clássico prato com feijão, arroz, carne de sol e muito queijo.', 45.00, 0, 1); -- 1

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Bife a Cavalo', 'Suculenta carne acompanhada de arroz, fritas e ovo frito.', 38.00, 0, 1); -- 2

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Frango Fit Grelhado', 'Peito de frango grelhado na manteiga com salada fresca.', 32.00, 0, 1); -- 3

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Peixe Frito com Pirão', 'Peixe da região frito na hora, acompanha pirão e arroz.', 42.00, 0, 1); -- 4

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Escondidinho de Carne', 'Purê cremoso de batata recheado com carne de sol.', 39.00, 0, 1); -- 5

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Baião de Dois', 'Mistura perfeita de arroz, feijão e pedaços de queijo coalho.', 36.00, 0, 1); -- 6

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Estrogonofe de Frango', 'Estrogonofe cremoso acompanhado de arroz e batata.', 34.00, 0, 1); -- 7

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Macarronada Rústica', 'Massa fresca ao molho de tomate com pedaços de carne.', 37.00, 0, 1); -- 8

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Salada Completa', 'Mix de folhas verdes, tomate, cebola e frango desfiado.', 28.00, 0, 1); -- 9

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Prato do Trabalhador', 'Arroz, feijão tropeiro e dois suculentos filés de frango.', 25.00, 0, 1); -- 10


-- =========================================================
-- 3. INSERINDO PRATOS DE JANTAR (Período = 1) (IDs de 11 a 20)
-- =========================================================
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Risoto de Camarão', 'Arroz arbóreo cremoso com camarões grandes e parmesão.', 68.00, 1, 1); -- 11

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Filé Mignon ao Molho', 'Medalhões de mignon banhados em molho de vinho e batatas.', 85.00, 1, 1); -- 12

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Salmão com Ervas', 'Posta de salmão grelhada com azeite trufado e salada.', 75.00, 1, 1); -- 13

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Camarão na Moranga', 'Camarões puxados no creme de queijo dentro da moranga.', 92.00, 1, 1); -- 14

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Espaguete ao Pesto', 'Massa artesanal ao molho pesto com tomates confitados.', 54.00, 1, 1); -- 15

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Moqueca Baiana', 'Moqueca de peixe e camarão no azeite de dendê.', 88.00, 1, 1); -- 16

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Frango à Parmegiana', 'Peito de frango empanado, coberto com queijo e molho.', 48.00, 1, 1); -- 17

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Sopa de Cebola', 'Clássica sopa francesa gratinada com queijo parmesão.', 42.00, 1, 1); -- 18

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Tábua de Frios', 'Seleção de queijos e tomates para compartilhar.', 60.00, 1, 1); -- 19

INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo) 
VALUES ('Lasanha aos 4 Queijos', 'Massa artesanal intercalada com muito queijo derretido.', 58.00, 1, 1); -- 20


-- =========================================================
-- 4. VINCULANDO INGREDIENTES AOS PRATOS (A mágica acontecer!)
-- =========================================================
-- Rubacão (1) = Arroz(1), Feijão(2), Carne Sol(3), Queijo Coalho(9)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 1), (2, 1), (3, 1), (9, 1);

-- Bife a Cavalo (2) = Arroz(1), Batata(11), Filé(15)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 2), (11, 2), (15, 2);

-- Frango Fit (3) = Frango(6), Alface(12), Tomate(8)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (6, 3), (12, 3), (8, 3);

-- Risoto Camarão (11) = Arroz(1), Camarão(5), Parmesão(10)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (1, 11), (5, 11), (10, 11);

-- Filé Mignon (12) = Batata(11), Filé(15)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (11, 12), (15, 12);

-- Espaguete Pesto (15) = Massa(7), Tomate(8), Parmesão(10), Azeite(13)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (7, 15), (8, 15), (10, 15), (13, 15);

-- Moqueca (16) = Peixe(4), Camarão(5), Cebola(14), Tomate(8)
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES (4, 16), (5, 16), (14, 16), (8, 16);