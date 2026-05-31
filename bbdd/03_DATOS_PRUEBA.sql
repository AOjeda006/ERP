-- ============================================
-- SCRIPT DE DATOS DE PRUEBA
-- ERP_PedidosProveedores (Esquema Simplificado)
-- AUTOR: Andrés Ojeda
-- FECHA: 2026-02-18
--
-- CONTENIDO:
--   5 Categorías
--   10 Proveedores
--   110 Productos (10 únicos por proveedor + 10 compartidos)
--   ProductosProveedores (10 únicos/proveedor + 10 compartidos entre varios)
-- ============================================

-- =============================================
-- CATEGORÍAS DE PRODUCTO (5)
-- =============================================
SET IDENTITY_INSERT CategoriasProducto ON;

INSERT INTO CategoriasProducto (CategoriaID, NombreCategoria, Descripcion) VALUES
(1, 'Material de Oficina',     'Papel, bolígrafos, carpetas, archivadores y consumibles de escritorio'),
(2, 'Electrónica',             'Equipos informáticos, periféricos, cables y componentes electrónicos'),
(3, 'Mobiliario',              'Mesas, sillas, estanterías y mobiliario de oficina'),
(4, 'Limpieza e Higiene',      'Productos de limpieza, desinfectantes y material higiénico'),
(5, 'Alimentación y Catering', 'Café, agua, snacks y suministros para office y eventos');

SET IDENTITY_INSERT CategoriasProducto OFF;

-- =============================================
-- PROVEEDORES (10)
-- =============================================
SET IDENTITY_INSERT Proveedores ON;

INSERT INTO Proveedores (ProveedorID, CIF, RazonSocial, NombreComercial, Direccion, Ciudad, Provincia, Telefono, Email, PersonaContacto) VALUES
(1,  'B41000001', 'Suministros Andaluces S.L.',       'SumiAndaluz',     'C/ Sierpes 42',              'Sevilla',     'Sevilla',     '954110001', 'ventas@sumiandaluz.es',      'María García López'),
(2,  'B41000002', 'Tecnología del Sur S.A.',           'TecnoSur',        'Av. de la Palmera 18',       'Sevilla',     'Sevilla',     '954110002', 'info@tecnosur.es',           'Carlos Ruiz Martín'),
(3,  'B29000003', 'Distribuciones Costa Sol S.L.',     'DistriCosta',     'C/ Larios 7',                'Málaga',      'Málaga',      '952330003', 'pedidos@districosta.es',     'Ana Fernández Díaz'),
(4,  'B14000004', 'Oficinas y Más Córdoba S.L.',       'OfiMásCor',       'Av. Gran Capitán 25',        'Córdoba',     'Córdoba',     '957440004', 'comercial@ofimascor.es',     'Pedro Jiménez Ruiz'),
(5,  'B18000005', 'Granadina de Suministros S.A.',     'GranaSumi',       'C/ Recogidas 33',            'Granada',     'Granada',     '958550005', 'ventas@granasumi.es',        'Laura Moreno Castro'),
(6,  'B23000006', 'Jaén Distribución Global S.L.',     'JaénDistri',      'Paseo de la Estación 12',    'Jaén',        'Jaén',        '953660006', 'info@jaendistri.es',         'Francisco López Ruiz'),
(7,  'B11000007', 'Bahía Logística Cádiz S.A.',        'BahíaLog',        'Av. de Andalucía 50',        'Cádiz',       'Cádiz',       '956770007', 'logistica@bahialog.es',      'Carmen Navarro Gil'),
(8,  'B04000008', 'Almería Proveedores Integrales S.L.','AlmeProv',       'C/ Tiendas 8',               'Almería',     'Almería',     '950880008', 'contacto@almeprov.es',       'José Martínez Soto'),
(9,  'B21000009', 'Huelva Suministros Express S.L.',   'HuelvaExpress',   'Av. Martín Alonso Pinzón 5', 'Huelva',      'Huelva',      '959990009', 'express@huelvasum.es',       'Isabel Romero Vega'),
(10, 'B41000010', 'Ibérica de Material Profesional S.A.','IbéricaPro',    'Polígono PISA, Nave 14',     'Mairena del Aljarafe', 'Sevilla', '954100010', 'pro@ibericapro.es',   'Antonio Delgado Paz');

SET IDENTITY_INSERT Proveedores OFF;

-- =============================================
-- PRODUCTOS (110)
-- Productos 1-100: 10 únicos por proveedor
-- Productos 101-110: compartidos entre varios proveedores
-- =============================================
SET IDENTITY_INSERT Productos ON;

-- ── Proveedor 1 (SumiAndaluz) → Productos 1-10: Material de Oficina ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(1,  1, 'OFI-PAP-001', 'Paquete Folios A4 500 uds',          'Papel blanco 80g/m² para impresora',         'Paquete',  3.50),
(2,  1, 'OFI-BOL-002', 'Caja Bolígrafos BIC Azul x50',       'Bolígrafos de tinta azul punta media',       'Caja',     12.90),
(3,  1, 'OFI-CAR-003', 'Carpeta Clasificadora A4',            'Carpeta con 12 separadores de colores',      'Unidad',   4.75),
(4,  1, 'OFI-GRA-004', 'Grapadora Metálica 24/6',             'Grapadora de sobremesa capacidad 25 hojas',  'Unidad',   8.20),
(5,  1, 'OFI-TIJ-005', 'Tijeras Oficina 21cm',                'Tijeras de acero inoxidable mango ergonómico','Unidad',   3.10),
(6,  1, 'OFI-POS-006', 'Pack Post-it 76x76mm x12',            'Notas adhesivas amarillas, 12 blocs',        'Pack',     9.60),
(7,  1, 'OFI-ROT-007', 'Set Rotuladores Pizarra x4',          'Rotuladores borrables negro/rojo/azul/verde','Set',      5.40),
(8,  1, 'OFI-SOB-008', 'Caja Sobres Blancos C5 x500',         'Sobres 162x229mm sin ventana',               'Caja',     18.50),
(9,  1, 'OFI-CIN-009', 'Cinta Adhesiva Transparente x6',      'Cinta 19mm x 33m, pack de 6 rollos',        'Pack',     4.30),
(10, 1, 'OFI-ARC-010', 'Archivador AZ Lomo 75mm',             'Archivador de palanca tamaño folio',         'Unidad',   2.85);

-- ── Proveedor 2 (TecnoSur) → Productos 11-20: Electrónica ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(11, 2, 'ELE-RAT-011', 'Ratón Óptico USB',                    'Ratón con cable 1200DPI, plug & play',       'Unidad',   7.90),
(12, 2, 'ELE-TEC-012', 'Teclado USB Español',                 'Teclado membrana layout español QWERTY',     'Unidad',   12.50),
(13, 2, 'ELE-HUB-013', 'Hub USB 3.0 4 Puertos',              'Concentrador USB con alimentación externa',   'Unidad',   14.90),
(14, 2, 'ELE-CAB-014', 'Cable HDMI 2m',                       'Cable HDMI 2.0 4K macho-macho',              'Unidad',   6.70),
(15, 2, 'ELE-AUR-015', 'Auriculares con Micrófono USB',       'Auriculares estéreo para videoconferencia',  'Unidad',   19.90),
(16, 2, 'ELE-WEB-016', 'Webcam HD 1080p',                     'Cámara web con autofocus y micrófono',       'Unidad',   29.90),
(17, 2, 'ELE-PEN-017', 'Pendrive USB 64GB',                   'Memoria flash USB 3.0 64GB',                 'Unidad',   8.50),
(18, 2, 'ELE-REG-018', 'Regleta 6 Tomas + Protección',       'Regleta con protección contra sobretensiones','Unidad',   15.40),
(19, 2, 'ELE-PIL-019', 'Pack Pilas AA Alcalinas x20',         'Pilas LR6 1.5V larga duración',              'Pack',     11.20),
(20, 2, 'ELE-CAR-020', 'Cargador Universal USB-C 65W',        'Cargador PD compatible con portátiles',      'Unidad',   24.90);

-- ── Proveedor 3 (DistriCosta) → Productos 21-30: Mobiliario ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(21, 3, 'MOB-SIL-021', 'Silla Oficina Ergonómica Basic',      'Silla giratoria con respaldo malla',         'Unidad',   89.90),
(22, 3, 'MOB-MES-022', 'Mesa Escritorio 120x60cm',            'Mesa con estructura metálica tablero melamina','Unidad',  75.00),
(23, 3, 'MOB-EST-023', 'Estantería Metálica 5 Baldas',        'Estantería galvanizada 180x90x40cm',         'Unidad',   49.90),
(24, 3, 'MOB-CAJ-024', 'Cajonera con Ruedas 3 Cajones',       'Cajonera móvil bajo mesa con cerradura',     'Unidad',   65.00),
(25, 3, 'MOB-PER-025', 'Perchero de Pie Metálico',            'Perchero 8 ganchos altura 175cm',            'Unidad',   22.50),
(26, 3, 'MOB-PAP-026', 'Papelera Metálica 15L',               'Papelera de rejilla negra',                  'Unidad',   6.90),
(27, 3, 'MOB-ARM-027', 'Armario Bajo con Puertas',            'Armario 80x42x78cm con cerradura',           'Unidad',   110.00),
(28, 3, 'MOB-REP-028', 'Reposapiés Ergonómico',               'Reposapiés ajustable antideslizante',        'Unidad',   18.90),
(29, 3, 'MOB-BIO-029', 'Biombo Separador 160x180cm',          'Biombo acústico tapizado gris',              'Unidad',   95.00),
(30, 3, 'MOB-PIZ-030', 'Pizarra Blanca Magnética 90x60',      'Pizarra lacada con bandeja y rotuladores',   'Unidad',   32.50);

-- ── Proveedor 4 (OfiMásCor) → Productos 31-40: Material de Oficina ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(31, 1, 'OFI-ETI-031', 'Etiquetas Adhesivas A4 x100',         'Hojas de etiquetas para impresora láser',    'Paquete',  14.20),
(32, 1, 'OFI-FUN-032', 'Fundas Multitaladro A4 x100',         'Fundas transparentes de polipropileno',      'Paquete',  5.60),
(33, 1, 'OFI-CUT-033', 'Cúter Profesional 18mm',              'Cúter con cuchilla retráctil y bloqueo',     'Unidad',   3.90),
(34, 1, 'OFI-PEG-034', 'Pegamento en Barra 40g x6',           'Adhesivo no tóxico secado rápido',           'Pack',     7.80),
(35, 1, 'OFI-TAC-035', 'Taco Calendario Sobremesa 2026',       'Taco diario con efemérides',                 'Unidad',   4.50),
(36, 1, 'OFI-BAN-036', 'Bandeja Sobremesa Apilable x3',        'Set 3 bandejas plástico negro',             'Set',      9.90),
(37, 1, 'OFI-CLP-037', 'Clips Mariposa 50mm x50',             'Clips sujetapapeles metálicos grandes',      'Caja',     3.20),
(38, 1, 'OFI-TAM-038', 'Tampón Tinta Azul',                    'Tampón de entintado para sellos',           'Unidad',   5.10),
(39, 1, 'OFI-SEL-039', 'Sello Fechador Automático',            'Sello con fecha ajustable 4mm',             'Unidad',   11.50),
(40, 1, 'OFI-CAL-040', 'Calculadora Sobremesa 12 Dígitos',     'Calculadora solar con tecla doble cero',    'Unidad',   8.90);

-- ── Proveedor 5 (GranaSumi) → Productos 41-50: Limpieza e Higiene ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(41, 4, 'LIM-LEJ-041', 'Lejía Concentrada 5L',                'Lejía desinfectante uso profesional',        'Garrafa',  4.20),
(42, 4, 'LIM-FRE-042', 'Fregasuelos Aroma Pino 5L',           'Limpiador multiusos para suelos',            'Garrafa',  5.80),
(43, 4, 'LIM-LIM-043', 'Limpiacristales Spray 750ml',         'Limpiador de cristales sin residuos',        'Unidad',   2.90),
(44, 4, 'LIM-PAP-044', 'Papel Higiénico Industrial x18',      'Rollos jumbo 300m doble capa',               'Pack',     22.50),
(45, 4, 'LIM-JAB-045', 'Jabón Manos Dosificador 5L',          'Jabón líquido pH neutro con dosificador',    'Garrafa',  8.90),
(46, 4, 'LIM-GEL-046', 'Gel Hidroalcohólico 500ml x6',        'Gel desinfectante 70% alcohol',              'Pack',     15.60),
(47, 4, 'LIM-BOL-047', 'Bolsas Basura 100L x25',              'Bolsas resistentes galga 120 negras',        'Paquete',  4.50),
(48, 4, 'LIM-MOC-048', 'Mopa Industrial Microfibra 60cm',     'Mopa profesional lavable con soporte',       'Unidad',   12.30),
(49, 4, 'LIM-GUA-049', 'Guantes Nitrilo Talla M x100',        'Guantes desechables sin polvo',              'Caja',     9.70),
(50, 4, 'LIM-TOA-050', 'Toallitas Desinfectantes x200',       'Toallitas húmedas bactericidas',             'Bote',     7.40);

-- ── Proveedor 6 (JaénDistri) → Productos 51-60: Alimentación y Catering ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(51, 5, 'ALI-CAF-051', 'Café Molido Natural 1kg',              'Café 100% arábica tueste natural',           'Paquete',  9.80),
(52, 5, 'ALI-AZU-052', 'Azúcar Blanco Sobres x500',           'Sobres individuales de 8g',                  'Caja',     6.50),
(53, 5, 'ALI-AGU-053', 'Agua Mineral 1.5L x12',               'Botellas de agua sin gas',                   'Pack',     4.20),
(54, 5, 'ALI-TEB-054', 'Té Variado Caja 100 Bolsitas',        'Surtido: manzanilla, verde, negro, menta',  'Caja',     7.30),
(55, 5, 'ALI-GAL-055', 'Galletas Surtido Office x48',          'Galletas envasadas individualmente',        'Caja',     12.90),
(56, 5, 'ALI-LEH-056', 'Leche Semidesnatada Cápsulas x50',    'Cápsulas de leche para café',                'Caja',     5.40),
(57, 5, 'ALI-VAS-057', 'Vasos Cartón 200ml x100',              'Vasos desechables para bebidas calientes',  'Paquete',  3.80),
(58, 5, 'ALI-SER-058', 'Servilletas Papel x500',               'Servilletas 1 capa 30x30cm blancas',        'Paquete',  4.10),
(59, 5, 'ALI-ACE-059', 'Aceite Oliva Virgen Extra 5L',         'AOVE de Jaén, acidez máx. 0.4°',            'Garrafa',  28.50),
(60, 5, 'ALI-SAL-060', 'Sal y Pimienta Sobres x200',           'Condimentos individuales para catering',     'Caja',     5.90);

-- ── Proveedor 7 (BahíaLog) → Productos 61-70: Electrónica ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(61, 2, 'ELE-MON-061', 'Monitor 24" Full HD IPS',              'Monitor LED IPS 1920x1080 HDMI/VGA',        'Unidad',   139.00),
(62, 2, 'ELE-DIS-062', 'Disco Duro Externo 1TB USB 3.0',      'HDD portátil 2.5" con cable incluido',      'Unidad',   49.90),
(63, 2, 'ELE-SSD-063', 'SSD Interno 512GB SATA',              'Unidad de estado sólido 2.5" 550MB/s',      'Unidad',   42.50),
(64, 2, 'ELE-ROU-064', 'Router WiFi 6 Dual Band',             'Router AX1500 con 4 antenas',                'Unidad',   59.90),
(65, 2, 'ELE-SWI-065', 'Switch de Red 8 Puertos Gigabit',     'Switch no gestionable 10/100/1000',          'Unidad',   22.90),
(66, 2, 'ELE-SAI-066', 'SAI 700VA / 360W',                    'Sistema alimentación ininterrumpida 2 tomas','Unidad',   65.00),
(67, 2, 'ELE-IMP-067', 'Impresora Láser Monocromo',           'Impresora láser B/N 30ppm WiFi',             'Unidad',   119.00),
(68, 2, 'ELE-TON-068', 'Tóner Compatible Negro',               'Tóner rendimiento 3000 páginas',            'Unidad',   18.90),
(69, 2, 'ELE-ALF-069', 'Alfombrilla Ratón XL 80x30cm',       'Alfombrilla gaming base antideslizante',     'Unidad',   9.90),
(70, 2, 'ELE-LAM-070', 'Lámpara LED Escritorio Regulable',    'Flexo LED 10W con 3 temperaturas de color',  'Unidad',   24.50);

-- ── Proveedor 8 (AlmeProv) → Productos 71-80: Mobiliario ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(71, 3, 'MOB-SIE-071', 'Silla Confidente Apilable',           'Silla visitante tapizada sin brazos',        'Unidad',   35.00),
(72, 3, 'MOB-MRE-072', 'Mesa Reuniones Ovalada 240x120',      'Mesa de reuniones para 8 personas',          'Unidad',   290.00),
(73, 3, 'MOB-ALE-073', 'Armario Alto 2 Puertas con Llave',    'Armario 80x42x180cm melamina blanca',        'Unidad',   165.00),
(74, 3, 'MOB-SOF-074', 'Sofá Recepción 2 Plazas',             'Sofá tapizado en polipiel negra',            'Unidad',   220.00),
(75, 3, 'MOB-MCA-075', 'Mesa Auxiliar Café 60x60cm',           'Mesita baja para zona de espera',           'Unidad',   42.00),
(76, 3, 'MOB-COL-076', 'Columna Archivador Rotativo',          'Archivador giratorio 4 niveles',            'Unidad',   85.00),
(77, 3, 'MOB-ATR-077', 'Atril Presentaciones Regulable',       'Atril de pie con bandeja porta-documentos', 'Unidad',   55.00),
(78, 3, 'MOB-TAB-078', 'Tablón Corcho 90x60cm',               'Tablón de anuncios con marco aluminio',     'Unidad',   14.90),
(79, 3, 'MOB-RUE-079', 'Ruedas Silla Oficina x5',             'Set de recambio ruedas 11mm universales',   'Set',       8.50),
(80, 3, 'MOB-ELE-080', 'Elevador Monitor Madera',              'Soporte monitor con cajón organizador',     'Unidad',   19.90);

-- ── Proveedor 9 (HuelvaExpress) → Productos 81-90: Limpieza e Higiene ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(81, 4, 'LIM-DES-081', 'Desengrasante Industrial 5L',          'Desengrasante concentrado biodegradable',   'Garrafa',  7.90),
(82, 4, 'LIM-AMB-082', 'Ambientador Spray Lavanda 400ml',      'Ambientador en aerosol larga duración',    'Unidad',   3.50),
(83, 4, 'LIM-ESC-083', 'Escoba Interior con Recogedor',        'Escoba de cerdas suaves con palo',         'Set',      6.20),
(84, 4, 'LIM-CUB-084', 'Cubo Fregona con Escurridor 16L',      'Cubo con escurridor de palanca',           'Unidad',   9.80),
(85, 4, 'LIM-BAY-085', 'Bayetas Microfibra 40x40 x10',         'Bayetas multiusos lavables',               'Pack',     5.40),
(86, 4, 'LIM-ROL-086', 'Rollo Papel Secamanos x6',             'Rollos azul 2 capas 150m autocortante',    'Pack',     16.90),
(87, 4, 'LIM-DIS-087', 'Dispensador Jabón Pared 1L',           'Dispensador ABS con cerradura',             'Unidad',   11.50),
(88, 4, 'LIM-APR-088', 'Abrillantador Suelos 5L',              'Abrillantador autosecante para terrazo',   'Garrafa',  8.70),
(89, 4, 'LIM-REC-089', 'Recambio Mopa Algodón 60cm',           'Recambio lavable para mopa industrial',    'Unidad',   4.90),
(90, 4, 'LIM-CEP-090', 'Cepillo WC con Soporte',               'Cepillo de baño con soporte acero inox.',  'Unidad',   5.70);

-- ── Proveedor 10 (IbéricaPro) → Productos 91-100: Alimentación y Catering ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(91,  5, 'ALI-CAP-091', 'Cápsulas Café Compatibles x100',       'Cápsulas intensidad 8 compatibles Nespresso','Caja',   22.50),
(92,  5, 'ALI-CHO-092', 'Cacao Soluble Instantáneo 1kg',        'Chocolate a la taza instantáneo',            'Bote',   6.80),
(93,  5, 'ALI-ZUM-093', 'Zumo Naranja Brick 1L x12',            'Zumo 100% exprimido sin azúcar añadido',    'Pack',    14.40),
(94,  5, 'ALI-BAR-094', 'Barritas Cereales Surtido x24',        'Barritas energéticas envasadas',            'Caja',    11.90),
(95,  5, 'ALI-FRU-095', 'Frutos Secos Mix 1kg',                 'Mezcla de almendras, nueces y anacardos',   'Bolsa',   15.70),
(96,  5, 'ALI-PLA-096', 'Platos Cartón 22cm x100',              'Platos desechables biodegradables',         'Paquete',  6.20),
(97,  5, 'ALI-CUB-097', 'Cubiertos Madera Set x100',            'Tenedor+cuchillo+cuchara biodegradables',   'Set',     8.90),
(98,  5, 'ALI-EDU-098', 'Edulcorante Sobres x500',              'Edulcorante sin calorías uso individual',   'Caja',    7.50),
(99,  5, 'ALI-INF-099', 'Infusiones Surtido x60',               'Manzanilla, tila, poleo, rooibos',         'Caja',    8.40),
(100, 5, 'ALI-MAN-100', 'Mantequilla Porciones x100',           'Porciones individuales 10g refrigeradas',   'Caja',    12.30);

-- ── Productos 101-110: COMPARTIDOS entre varios proveedores ──
INSERT INTO Productos (ProductoID, CategoriaID, CodigoProducto, NombreProducto, Descripcion, UnidadMedida, PrecioUnitario) VALUES
(101, 1, 'COM-PAP-101', 'Paquete Folios A3 500 uds',           'Papel blanco A3 80g/m² multiusos',          'Paquete',  6.90),
(102, 2, 'COM-TON-102', 'Tóner Universal Negro HP',             'Tóner compatible HP 26A rendimiento 3100p', 'Unidad',   21.50),
(103, 1, 'COM-ROT-103', 'Rotuladores Fluorescentes x6',        'Pack 6 colores punta biselada',              'Pack',     4.80),
(104, 4, 'COM-GEL-104', 'Gel Manos Antibacteriano 1L',         'Gel con dosificador pH neutro',              'Unidad',   6.50),
(105, 2, 'COM-PEN-105', 'Pendrive USB-C 128GB',                 'Memoria dual USB-A/USB-C 3.2',              'Unidad',   14.90),
(106, 5, 'COM-AGU-106', 'Bidón Agua Fuente 18.9L',             'Bidón recambio para fuente de agua',         'Bidón',    5.90),
(107, 3, 'COM-SIL-107', 'Silla Gaming Ergonómica',              'Silla reclinable 135° con reposabrazos 4D', 'Unidad',   159.00),
(108, 4, 'COM-PAH-108', 'Papel Higiénico Doméstico x96',       'Rollos doble capa suave 96 unidades',       'Pack',     34.90),
(109, 1, 'COM-AGE-109', 'Agenda 2026 Día/Página A5',           'Agenda anual tapas polipiel azul',           'Unidad',   9.90),
(110, 2, 'COM-CAB-110', 'Cable Ethernet Cat6 5m',               'Cable red RJ45 Cat6 UTP azul',              'Unidad',   4.50);

SET IDENTITY_INSERT Productos OFF;

-- =============================================
-- PRODUCTOS-PROVEEDORES
--
-- Sección A: 100 relaciones 1:1 (cada proveedor con sus 10 productos únicos)
-- Sección B: 10 productos compartidos entre varios proveedores (con precios distintos)
-- =============================================

-- ── Sección A: Productos exclusivos por proveedor ──

-- Proveedor 1 (SumiAndaluz) → Productos 1-10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(1,  1, 3.20,  2),
(2,  1, 11.80, 2),
(3,  1, 4.30,  3),
(4,  1, 7.50,  3),
(5,  1, 2.80,  2),
(6,  1, 8.90,  2),
(7,  1, 4.90,  3),
(8,  1, 17.00, 4),
(9,  1, 3.80,  2),
(10, 1, 2.50,  2);

-- Proveedor 2 (TecnoSur) → Productos 11-20
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(11, 2, 7.20,  3),
(12, 2, 11.50, 3),
(13, 2, 13.50, 4),
(14, 2, 6.00,  2),
(15, 2, 18.20, 3),
(16, 2, 27.50, 5),
(17, 2, 7.80,  2),
(18, 2, 14.00, 3),
(19, 2, 10.20, 2),
(20, 2, 22.90, 4);

-- Proveedor 3 (DistriCosta) → Productos 21-30
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(21, 3, 82.00,  7),
(22, 3, 68.00,  7),
(23, 3, 45.00,  5),
(24, 3, 59.00,  7),
(25, 3, 20.00,  5),
(26, 3, 6.20,   3),
(27, 3, 99.00,  10),
(28, 3, 17.00,  5),
(29, 3, 86.00,  10),
(30, 3, 29.50,  5);

-- Proveedor 4 (OfiMásCor) → Productos 31-40
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(31, 4, 13.00, 3),
(32, 4, 5.10,  2),
(33, 4, 3.50,  2),
(34, 4, 7.10,  3),
(35, 4, 4.00,  2),
(36, 4, 9.00,  3),
(37, 4, 2.90,  2),
(38, 4, 4.60,  2),
(39, 4, 10.50, 4),
(40, 4, 8.10,  3);

-- Proveedor 5 (GranaSumi) → Productos 41-50
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(41, 5, 3.80,  2),
(42, 5, 5.20,  2),
(43, 5, 2.50,  2),
(44, 5, 20.50, 3),
(45, 5, 8.10,  3),
(46, 5, 14.20, 3),
(47, 5, 4.00,  2),
(48, 5, 11.20, 4),
(49, 5, 8.80,  3),
(50, 5, 6.70,  2);

-- Proveedor 6 (JaénDistri) → Productos 51-60
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(51, 6, 8.90,  3),
(52, 6, 5.80,  2),
(53, 6, 3.70,  2),
(54, 6, 6.60,  3),
(55, 6, 11.80, 3),
(56, 6, 4.80,  2),
(57, 6, 3.40,  2),
(58, 6, 3.70,  2),
(59, 6, 26.00, 4),
(60, 6, 5.30,  2);

-- Proveedor 7 (BahíaLog) → Productos 61-70
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(61, 7, 128.00, 5),
(62, 7, 45.90,  4),
(63, 7, 38.90,  4),
(64, 7, 54.90,  5),
(65, 7, 20.50,  3),
(66, 7, 59.00,  5),
(67, 7, 109.00, 7),
(68, 7, 17.20,  3),
(69, 7, 9.00,   2),
(70, 7, 22.50,  4);

-- Proveedor 8 (AlmeProv) → Productos 71-80
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(71, 8, 31.00,  5),
(72, 8, 265.00, 10),
(73, 8, 150.00, 10),
(74, 8, 199.00, 12),
(75, 8, 38.00,  5),
(76, 8, 78.00,  7),
(77, 8, 50.00,  5),
(78, 8, 13.50,  4),
(79, 8, 7.50,   3),
(80, 8, 18.00,  4);

-- Proveedor 9 (HuelvaExpress) → Productos 81-90
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(81, 9, 7.10,  2),
(82, 9, 3.10,  1),
(83, 9, 5.60,  2),
(84, 9, 8.90,  2),
(85, 9, 4.80,  1),
(86, 9, 15.20, 2),
(87, 9, 10.40, 3),
(88, 9, 7.90,  2),
(89, 9, 4.40,  1),
(90, 9, 5.10,  2);

-- Proveedor 10 (IbéricaPro) → Productos 91-100
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(91,  10, 20.50, 3),
(92,  10, 6.10,  2),
(93,  10, 13.20, 3),
(94,  10, 10.80, 3),
(95,  10, 14.30, 4),
(96,  10, 5.60,  2),
(97,  10, 8.10,  3),
(98,  10, 6.80,  2),
(99,  10, 7.60,  3),
(100, 10, 11.20, 4);

-- ── Sección B: Productos compartidos entre varios proveedores ──
-- Cada producto compartido (101-110) es ofrecido por 3-5 proveedores con precios distintos

-- Producto 101: Folios A3 → Proveedores 1, 4, 6, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(101, 1,  6.30,  2),
(101, 4,  6.50,  3),
(101, 6,  6.80,  4),
(101, 10, 5.90,  3);

-- Producto 102: Tóner HP → Proveedores 2, 7, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(102, 2,  19.80, 3),
(102, 7,  20.50, 4),
(102, 10, 18.90, 3);

-- Producto 103: Rotuladores Fluo → Proveedores 1, 4, 5, 9
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(103, 1, 4.40, 2),
(103, 4, 4.20, 2),
(103, 5, 4.60, 3),
(103, 9, 4.50, 1);

-- Producto 104: Gel Antibacteriano → Proveedores 5, 8, 9
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(104, 5, 5.90, 2),
(104, 8, 6.20, 3),
(104, 9, 5.70, 1);

-- Producto 105: Pendrive USB-C → Proveedores 2, 3, 7, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(105, 2,  13.50, 3),
(105, 3,  14.20, 4),
(105, 7,  13.80, 3),
(105, 10, 12.90, 2);

-- Producto 106: Bidón Agua → Proveedores 5, 6, 9, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(106, 5,  5.50, 2),
(106, 6,  5.20, 1),
(106, 9,  5.60, 1),
(106, 10, 5.30, 2);

-- Producto 107: Silla Gaming → Proveedores 3, 7, 8
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(107, 3, 145.00, 7),
(107, 7, 149.00, 8),
(107, 8, 139.00, 10);

-- Producto 108: Papel Higiénico Pack → Proveedores 5, 8, 9
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(108, 5, 31.90, 3),
(108, 8, 33.50, 4),
(108, 9, 30.80, 2);

-- Producto 109: Agenda 2026 → Proveedores 1, 4, 6, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(109, 1,  9.10, 2),
(109, 4,  9.30, 3),
(109, 6,  9.50, 3),
(109, 10, 8.80, 2);

-- Producto 110: Cable Ethernet → Proveedores 2, 3, 7, 8, 10
INSERT INTO ProductosProveedores (ProductoID, ProveedorID, PrecioProveedor, TiempoEntregaDias) VALUES
(110, 2,  4.00, 2),
(110, 3,  4.20, 3),
(110, 7,  3.90, 2),
(110, 8,  4.30, 3),
(110, 10, 3.70, 2);

-- =============================================
-- VERIFICACIÓN RÁPIDA
-- =============================================
-- Descomentar para comprobar:
-- SELECT 'Categorías' AS Tabla, COUNT(*) AS Total FROM CategoriasProducto;
-- SELECT 'Proveedores' AS Tabla, COUNT(*) AS Total FROM Proveedores;
-- SELECT 'Productos' AS Tabla, COUNT(*) AS Total FROM Productos;
-- SELECT 'ProductosProveedores' AS Tabla, COUNT(*) AS Total FROM ProductosProveedores;
-- SELECT 'PP por proveedor' AS Info, p.RazonSocial, COUNT(*) AS Productos
--   FROM ProductosProveedores pp JOIN Proveedores p ON pp.ProveedorID = p.ProveedorID
--   GROUP BY p.RazonSocial ORDER BY p.RazonSocial;

PRINT 'Datos de prueba insertados correctamente.';
PRINT '  - 5 Categorías';
PRINT '  - 10 Proveedores';
PRINT '  - 110 Productos';
PRINT '  - 137 relaciones ProductosProveedores (100 exclusivas + 37 compartidas)';
