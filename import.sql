-- =========================================================
-- 1. INSERINDO AS MESAS 
-- =========================================================
INSERT INTO Mesas (Numero, CapacidadePessoas) VALUES 
(1, 4), (2, 4), (3, 6), (4, 4), (5, 3), 
(6, 2), (7, 2), (8, 2), (9, 6), (10, 4);

-- =========================================================
-- 2. INSERINDO INGREDIENTES (IDs gerados de 1 a 20)
-- =========================================================
INSERT INTO Ingredientes (Nome, IsAtivo) VALUES 
('Arroz Branco', 1),       -- 1
('Feijão Tropeiro', 1),    -- 2
('Carne de Sol', 1),       -- 3
('Peixe Fresco', 1),       -- 4
('Camarão Rosa', 1),       -- 5
('Peito de Frango', 1),    -- 6
('Massa Artesanal', 1),    -- 7
('Tomate Cereja', 1),      -- 8
('Queijo Coalho', 1),      -- 9
('Queijo Parmesão', 1),    -- 10
('Batata Rústica', 1),     -- 11
('Alface Americana', 1),   -- 12
('Azeite Trufado', 1),     -- 13
('Cebola Roxa', 1),        -- 14
('Filé Mignon', 1),        -- 15
('Linguiça Calabresa', 1), -- 16
('Bacon em Cubos', 1),     -- 17
('Farofa Crocante', 1),    -- 18
('Alho Dourado', 1),       -- 19
('Pimentão Amarelo', 1);   -- 20

-- =========================================================
-- 3. INSERINDO PRATOS DO CARDÁPIO (IDs gerados de 1 a 40)
-- Período: 0 = Almoço | 1 = Jantar
-- =========================================================
-- ALMOÇO (IDs 1 a 20)
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES 
('Rubacão Tocantinense', 'Clássico prato com feijão, arroz, carne de sol e muito queijo.', 45.00, 0, 1, '/img/almoco/rubacaoTocantinense.png'),
('Bife a Cavalo', 'Suculenta carne acompanhada de arroz, fritas e ovo frito.', 38.00, 0, 1, '/img/almoco/bifeCavalo.png'),
('Frango Fit Grelhado', 'Peito de frango grelhado na manteiga com salada fresca.', 32.00, 0, 1, '/img/almoco/frangoGrelhado.png'),
('Peixe Frito com Pirão', 'Peixe da região frito na hora, acompanha pirão e arroz.', 42.00, 0, 1, '/img/almoco/peixeFritoPirao.png'),
('Escondidinho de Carne', 'Purê cremoso de batata recheado com carne de sol.', 39.00, 0, 1, '/img/almoco/escondidoCarne.png'),
('Baião de Dois', 'Mistura perfeita de arroz, feijão e pedaços de queijo coalho.', 36.00, 0, 1, '/img/almoco/baiaoDois.png'),
('Estrogonofe de Frango', 'Estrogonofe cremoso acompanhado de arroz e batata.', 34.00, 0, 1, '/img/almoco/estrogonofeFrango.png'),
('Macarronada Rústica', 'Massa fresca ao molho de tomate com pedaços de carne.', 37.00, 0, 1, '/img/almoco/macarronadaRustica.png'),
('Salada Completa', 'Mix de folhas verdes, tomate, cebola e frango desfiado.', 28.00, 0, 1, '/img/almoco/saladaCompleta.png'),
('Prato do Trabalhador', 'Arroz, feijão tropeiro e dois suculentos filés de frango.', 25.00, 0, 1, '/img/almoco/pratoTrabalhador.png'),
('Feijoada Completa', 'Feijoada tradicional com arroz, farofa e couve.', 48.00, 0, 1, '/img/almoco/feijoadaCompleta.png'),
('Picadinho de Mignon', 'Picadinho de carne com arroz e farofa.', 42.00, 0, 1, '/img/almoco/picanhaMignon.png'),
('Bife Acebolado', 'Bife grelhado com muita cebola e arroz branco.', 35.00, 0, 1, '/img/almoco/bifeAcebolado.png'),
('Iscas de Peixe', 'Tirinhas de peixe empanadas com batata rústica.', 38.00, 0, 1, '/img/almoco/iscasPeixe.png'),
('Panqueca de Frango', 'Massa artesanal recheada com frango desfiado.', 30.00, 0, 1, '/img/almoco/panquecaFrango.png'),
('Omelete de Queijo', 'Omelete de queijo coalho acompanhado de salada.', 22.00, 0, 1, '/img/almoco/omeleteQueijo.png'),
('Salpicão de Frango', 'Salpicão refrescante com frango e legumes.', 26.00, 0, 1, '/img/almoco/salpicaoFrango.png'),
('Carne de Panela', 'Carne cozida lentamente com batatas e arroz.', 39.00, 0, 1, '/img/almoco/carnePanela.png'),
('Nhoque ao Sugo', 'Massa artesanal de nhoque ao molho de tomates frescos.', 34.00, 0, 1, '/img/almoco/nhoqueSugo.png'),
('Frango à Passarinho', 'Cortes de frango fritos com alho dourado e arroz.', 31.00, 0, 1, '/img/almoco/frangoPassarinho.png');

-- JANTAR (IDs 21 a 40)
INSERT INTO ItensCardapio (Nome, Descricao, PrecoBase, Periodo, IsAtivo, ImagemUrl) VALUES 
('Risoto de Camarão', 'Arroz arbóreo cremoso com camarões grandes e parmesão.', 68.00, 1, 1, '/img/jantar/risotoCamarao.png'),
('Filé Mignon ao Molho', 'Medalhões de mignon banhados em molho de vinho e batatas.', 85.00, 1, 1, '/img/jantar/fileMignonMolho.png'),
('Salmão com Ervas', 'Posta de salmão grelhada com azeite trufado e salada.', 75.00, 1, 1, '/img/jantar/salmaoErvas.png'),
('Camarão na Moranga', 'Camarões puxados no creme de queijo dentro da moranga.', 92.00, 1, 1, '/img/jantar/camaraoMoranga.png'),
('Espaguete ao Pesto', 'Massa artesanal ao molho pesto com tomates confitados.', 54.00, 1, 1, '/img/jantar/espaguetePesto.png'),
('Moqueca Baiana', 'Moqueca de peixe e camarão no azeite de dendê.', 88.00, 1, 1, '/img/jantar/moquecaBaiana.png'),
('Frango à Parmegiana', 'Peito de frango empanado, coberto com queijo e molho.', 48.00, 1, 1, '/img/jantar/frangoParmegiana.png'),
('Sopa de Cebola', 'Clássica sopa francesa gratinada com queijo parmesão.', 42.00, 1, 1, '/img/jantar/sopaCebola.png'),
('Tábua de Frios', 'Seleção de queijos e tomates para compartilhar.', 60.00, 1, 1, '/img/jantar/tabuaFrios.png'),
('Lasanha aos 4 Queijos', 'Massa artesanal intercalada com muito queijo derretido.', 58.00, 1, 1, '/img/jantar/lasanhaQuatroQueijos.png'),
('Pizza Margherita', 'Massa artesanal, tomate, queijo e manjericão.', 55.00, 1, 1, '/img/jantar/pizzaMargherita.png'),
('Hambúrguer Gourmet', 'Blend de filé mignon, bacon, queijo e batatas.', 45.00, 1, 1, '/img/jantar/hamburguer.png'),
('Caldo de Feijão', 'Caldo quente de feijão com bacon crocante e pão.', 28.00, 1, 1, '/img/jantar/caldoFeijao.png'),
('Ceviche de Peixe', 'Peixe fresco marinado com cebola roxa e limão.', 48.00, 1, 1, '/img/jantar/cevichePeixe.png'),
('Yakisoba Especial', 'Massa com frango, carne, legumes e molho de soja.', 52.00, 1, 1, '/img/jantar/yakisoba.png'),
('Fondue de Queijo', 'Mistura de queijos finos para mergulhar pães e carnes.', 110.00, 1, 1, '/img/jantar/fondueQueijo.png'),
('Tacos Mexicanos', 'Tortilhas recheadas com carne, alface e queijo.', 44.00, 1, 1, '/img/jantar/tacos.png'),
('Camarão Alho e Óleo', 'Porção de camarões salteados no alho dourado.', 65.00, 1, 1, '/img/jantar/camaraoAlhoOleo.png'),
('Risoto de Funghi', 'Arroz arbóreo com cogumelos e queijo parmesão.', 62.00, 1, 1, '/img/jantar/risotoFunghi.png'),
('Salada Caprese', 'Fatias de tomate fresco, queijo coalho e azeite.', 35.00, 1, 1, '/img/jantar/saladaCaprese.png');

-- =========================================================
-- 4. VINCULANDO INGREDIENTES AOS PRATOS
-- =========================================================
-- ALMOÇO
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES 
(1, 1), (2, 1), (3, 1), (9, 1),             -- 1. Rubacão
(1, 2), (11, 2), (15, 2),                   -- 2. Bife a Cavalo
(6, 3), (12, 3), (8, 3),                    -- 3. Frango Fit
(4, 4), (1, 4), (18, 4),                    -- 4. Peixe Frito
(11, 5), (3, 5), (9, 5),                    -- 5. Escondidinho
(1, 6), (2, 6), (3, 6), (9, 6),             -- 6. Baião
(6, 7), (1, 7), (11, 7),                    -- 7. Estrogonofe
(7, 8), (8, 8), (15, 8),                    -- 8. Macarronada
(12, 9), (8, 9), (14, 9), (6, 9),           -- 9. Salada
(1, 10), (2, 10), (6, 10),                  -- 10. Prato Trabalhador
(1, 11), (18, 11), (16, 11), (17, 11),      -- 11. Feijoada
(15, 12), (1, 12), (18, 12),                -- 12. Picadinho
(15, 13), (14, 13), (1, 13),                -- 13. Bife Acebolado
(4, 14), (11, 14),                          -- 14. Iscas de Peixe
(7, 15), (6, 15),                           -- 15. Panqueca
(9, 16), (12, 16), (8, 16),                 -- 16. Omelete Queijo
(6, 17), (20, 17), (14, 17),                -- 17. Salpicão
(15, 18), (11, 18), (1, 18),                -- 18. Carne Panela
(7, 19), (8, 19),                           -- 19. Nhoque
(6, 20), (19, 20), (1, 20);                 -- 20. Frango Passarinho

-- JANTAR
INSERT INTO IngredienteItemCardapio (IngredientesId, ItensCardapioId) VALUES 
(1, 21), (5, 21), (10, 21),                 -- 21. Risoto Camarão
(11, 22), (15, 22),                         -- 22. Filé Mignon
(4, 23), (13, 23), (12, 23),                -- 23. Salmão
(5, 24), (9, 24), (10, 24),                 -- 24. Camarão Moranga
(7, 25), (8, 25), (10, 25), (13, 25),       -- 25. Espaguete
(4, 26), (5, 26), (14, 26), (8, 26),        -- 26. Moqueca
(6, 27), (10, 27), (8, 27),                 -- 27. Frango Parmegiana
(14, 28), (10, 28),                         -- 28. Sopa Cebola
(9, 29), (10, 29), (8, 29),                 -- 29. Tábua Frios
(7, 30), (9, 30), (10, 30),                 -- 30. Lasanha Queijos
(7, 31), (8, 31), (9, 31), (10, 31),        -- 31. Pizza Margherita
(15, 32), (17, 32), (9, 32), (11, 32),      -- 32. Hambúrguer
(2, 33), (17, 33), (19, 33),                -- 33. Caldo Feijão
(4, 34), (14, 34), (20, 34),                -- 34. Ceviche
(7, 35), (6, 35), (15, 35), (20, 35),       -- 35. Yakisoba
(9, 36), (10, 36), (15, 36),                -- 36. Fondue
(7, 37), (15, 37), (12, 37), (9, 37),       -- 37. Tacos
(5, 38), (19, 38),                          -- 38. Camarão Alho/Óleo
(1, 39), (10, 39), (13, 39),                -- 39. Risoto Funghi
(8, 40), (9, 40), (13, 40);                 -- 40. Salada Caprese

-- =========================================================
-- 5. INSERINDO ENDEREÇOS (Baseado nos IDs do Identity criados no Program.cs)
-- =========================================================
INSERT INTO Enderecos (UsuarioId, CEP, Quadra, Alameda, Lote, Complemento, Referencia, IsAtivo) VALUES
(2, '77020000', '104 Sul', 'Alameda 1', '10', 'Casa', 'Perto do mercado', 1),
(2, '77020020', '204 Sul', 'Alameda 2', '15', 'Apto 101', 'Perto da farmácia', 1),
(3, '77020040', '306 Sul', 'Alameda 3', '20', '', 'Em frente a praça', 1),
(3, '77020060', '404 Sul', 'Alameda 4', '25', '', 'Ao lado da padaria', 1);

-- =========================================================
-- 6. INSERINDO SUGESTÕES DO CHEFE (Passadas, para popular o dashboard)
-- =========================================================
INSERT INTO SugestoesChefe (ItemCardapioId, DataSugestao, DescontoAplicado) VALUES
(1, '2026-03-20', 20.00), (21, '2026-03-25', 20.00), (32, '2026-04-08', 20.00),
(10, '2026-04-10', 20.00), (36, '2026-04-12', 20.00), (26, '2026-03-22', 20.00),
(3, '2026-03-26', 20.00), (30, '2026-04-04', 20.00), (4, '2026-04-07', 20.00),
(22, '2026-04-16', 20.00);

-- =========================================================
-- 7. INSERINDO ATENDIMENTOS (Herança TPH)
--
-- REGRAS DE STATUS:
-- Retirada: 0=EmProcesso, 1=PedidoPronto, 2=Cancelado
-- Delivery: 0=EmProcesso, 1=SaiuParaEntrega, 2=Entregue, 3=Cancelado
-- =========================================================
-- Atendimentos de PEDRO (IDs gerados de 1 a 10)
INSERT INTO Atendimentos (Discriminator, TaxaEntrega, Status, EnderecoEntregaId, NomeAplicativo) VALUES
('AtendimentoRetirada', 0.00, 1, NULL, NULL),          -- 1: Pronto (1)
('AtendimentoDeliveryApp', 6.00, 2, 1, 'iFood'),       -- 2: Entregue (2)
('AtendimentoDeliveryProprio', 8.00, 1, 2, NULL),      -- 3: SaiuParaEntrega (1) - NOVO ESTADO!
('AtendimentoDeliveryApp', 6.00, 3, 1, 'Aiqfome'),     -- 4: Cancelado (3) - Único cancelado do Pedro!
('AtendimentoDeliveryProprio', 8.00, 2, 2, NULL),      -- 5: Entregue (2)
('AtendimentoRetirada', 0.00, 1, NULL, NULL),          -- 6: Pronto (1)
('AtendimentoDeliveryApp', 4.00, 0, 1, 'iFood'),       -- 7: Em Processo (0)
('AtendimentoDeliveryProprio', 8.00, 1, 2, NULL),      -- 8: SaiuParaEntrega (1) - NOVO ESTADO!
('AtendimentoDeliveryApp', 6.00, 2, 1, 'Aiqfome'),     -- 9: Entregue (2)
('AtendimentoRetirada', 0.00, 0, NULL, NULL);          -- 10: Em Processo (0)

-- Atendimentos de LUCAS (IDs gerados de 11 a 20)
INSERT INTO Atendimentos (Discriminator, TaxaEntrega, Status, EnderecoEntregaId, NomeAplicativo) VALUES
('AtendimentoDeliveryProprio', 8.00, 2, 3, NULL),      -- 11: Entregue (2)
('AtendimentoDeliveryApp', 4.00, 1, 4, 'Aiqfome'),     -- 12: SaiuParaEntrega (1) - NOVO ESTADO!
('AtendimentoRetirada', 0.00, 2, NULL, NULL),          -- 13: Cancelado (2) - Único cancelado do Lucas!
('AtendimentoDeliveryProprio', 8.00, 2, 3, NULL),      -- 14: Entregue (2)
('AtendimentoDeliveryApp', 6.00, 2, 4, 'iFood'),       -- 15: Entregue (2)
('AtendimentoRetirada', 0.00, 1, NULL, NULL),          -- 16: Pronto (1)
('AtendimentoDeliveryProprio', 8.00, 1, 3, NULL),      -- 17: SaiuParaEntrega (1) - NOVO ESTADO!
('AtendimentoDeliveryApp', 4.00, 0, 4, 'Aiqfome'),     -- 18: Em Processo (0)
('AtendimentoRetirada', 0.00, 1, NULL, NULL),          -- 19: Pronto (1)
('AtendimentoDeliveryProprio', 8.00, 0, 3, NULL);      -- 20: Em Processo (0)

-- =========================================================
-- 8. INSERINDO PEDIDOS
-- Note que o IsCancelado = 1 é atrelado APENAS ao ID 4 (Pedro) e ID 13 (Lucas)
-- =========================================================
-- Pedidos PEDRO (IDs Atendimento de 1 a 10)
INSERT INTO Pedidos (UsuarioId, DataHora, PrecoFinal, IsCancelado, AtendimentoId) VALUES
(2, '2026-03-20 12:00:00', 72.00, 0, 1), (2, '2026-03-25 19:30:00', 135.40, 0, 2),
(2, '2026-03-28 13:00:00', 84.00, 0, 3), (2, '2026-04-02 20:00:00', 55.00, 1, 4), -- CANCELADO
(2, '2026-04-05 12:30:00', 52.00, 0, 5), (2, '2026-04-08 19:00:00', 80.00, 0, 6),
(2, '2026-04-10 11:30:00', 20.00, 0, 7), (2, '2026-04-12 21:00:00', 94.00, 0, 8),
(2, '2026-04-14 13:15:00', 46.00, 0, 9), (2, '2026-04-16 12:00:00', 90.00, 0, 10);

-- Pedidos LUCAS (IDs Atendimento de 11 a 20)
INSERT INTO Pedidos (UsuarioId, DataHora, PrecoFinal, IsCancelado, AtendimentoId) VALUES
(3, '2026-03-22 19:00:00', 78.40, 0, 11), (3, '2026-03-26 12:45:00', 55.20, 0, 12),
(3, '2026-03-30 20:30:00', 104.00, 1, 13), -- CANCELADO
(3, '2026-04-01 13:00:00', 42.00, 0, 14), (3, '2026-04-04 19:45:00', 98.80, 0, 15), 
(3, '2026-04-07 12:15:00', 33.60, 0, 16), (3, '2026-04-09 21:30:00', 68.00, 0, 17), 
(3, '2026-04-11 14:00:00', 72.00, 0, 18), (3, '2026-04-13 19:15:00', 88.00, 0, 19), 
(3, '2026-04-16 19:00:00', 76.00, 0, 20);

-- =========================================================
-- 9. INSERINDO ITENS NO PEDIDO
-- =========================================================
-- Itens de PEDRO
INSERT INTO ItensPedido (PedidoId, ItemCardapioId, Quantidade, PrecoUnitario) VALUES
(1, 1, 2, 36.00), (2, 21, 1, 54.40), (2, 23, 1, 75.00), (3, 2, 2, 38.00), (4, 31, 1, 55.00),
(5, 11, 1, 48.00), (6, 32, 2, 36.00), (7, 10, 1, 20.00), (8, 36, 1, 88.00), (9, 14, 1, 38.00), (10, 1, 2, 45.00);

-- Itens de LUCAS
INSERT INTO ItensPedido (PedidoId, ItemCardapioId, Quantidade, PrecoUnitario) VALUES
(11, 26, 1, 70.40), (12, 3, 2, 25.60), (13, 35, 2, 52.00), (14, 7, 1, 34.00),
(15, 30, 2, 46.40), (16, 4, 1, 33.60), (17, 29, 1, 60.00), (18, 19, 2, 34.00),
(19, 37, 2, 44.00), (20, 22, 1, 68.00);