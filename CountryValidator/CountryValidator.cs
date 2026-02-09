using CountryValidation.Countries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CountryValidation
{
    public class CountryValidator : ICountryValidator
    {
        static readonly IReadOnlyDictionary<Country, IdValidationAbstract> _supportedCountries;

        static readonly IReadOnlyDictionary<string, Country> _countriesByCode;

        static CountryValidator()
        {
            _supportedCountries = Load();
            _countriesByCode = getCountriesByCode(_supportedCountries.Keys);
        }

        private static IReadOnlyDictionary<string, Country> getCountriesByCode(IEnumerable<Country> countries)
        {
            var dict = countries
                .Where(c => c != Country.XX)
                .ToDictionary(
                    c => c.ToString().ToUpper(),
                    c => c,
                    StringComparer.OrdinalIgnoreCase
                );
            return dict;
        }

        public static bool TryGetCountryByCode(string code, out Country country)
            => _countriesByCode.TryGetValue(code, out country);

        public static bool IsCountrySupported(string countryCode)
        {
            if (TryGetCountryByCode(countryCode, out Country country))
                return true;
            else
                return false;
        }

        public static bool IsCountrySupported(Country country)
        {
            return _supportedCountries.ContainsKey(country);
        }

        public static IReadOnlyList<Country> SupportedCountryVals
            => _supportedCountries.Keys
                .Where(c => c != Country.XX)
                .ToList();

        public static List<string> SupportedCountries
            => SupportedCountryVals.Select(c => c.ToString()).ToList();

        private static IReadOnlyDictionary<Country, IdValidationAbstract> Load()
        {
            var assembly = typeof(CountryValidator).Assembly;
            var validatorTypes = assembly
                .GetTypes()
                .Where(t => typeof(IdValidationAbstract).IsAssignableFrom(t)
                    && !t.IsAbstract
                    && t.GetConstructor(Type.EmptyTypes) != null); // needs public parameterless ctor

            var dict = validatorTypes
                .Select(t => (IdValidationAbstract)Activator.CreateInstance(t)!)
                .ToDictionary(v => v.CountryCode, v => v);

            return dict;
        }

        private static Dictionary<Country, IdValidationAbstract> Load_brittle ()
        {
            Dictionary<Country, IdValidationAbstract> ssnCountries = new Dictionary<Country, IdValidationAbstract>
            {
                { Country.AD, new AndorraValidator() },
                { Country.AE, new UnitedArabEmiratesValidator() },
                { Country.AL, new AlbaniaValidator() },
                { Country.AM, new ArmeniaValidator() },
                { Country.AR, new ArgentinaValidator() },
                { Country.AT, new AustriaValidator() },
                { Country.AU, new AustraliaValidator() },
                { Country.AZ, new AzerbaijanValidator() },
                { Country.BA, new BosniaValidator() },
                { Country.BE, new BelgiumValidator() },
                { Country.BG, new BulgariaValidator() },
                { Country.BH, new BahrainValidator() },
                { Country.BO, new BoliviaValidator() },
                { Country.BR, new BrazilValidator() },
                { Country.BY, new BelarusValidator() },
                { Country.CA, new CanadaValidator() },
                { Country.CH, new SwitzerlandValidator() },
                { Country.CL, new ChileValidator() },
                { Country.CN, new ChinaValidator() },
                { Country.CO, new ColombiaValidator()},
                { Country.CR, new CostaRicaValidator()},
                { Country.CU, new CubaValidator()},
                { Country.CY, new CyprusValidator()},
                { Country.CZ, new CzechValidator()},
                { Country.DE, new GermanyValidator() },
                { Country.DK, new DenmarkValidator() },
                { Country.DO, new DominicanRepublicValidator() },
                { Country.EC, new EcuadorValidator() },
                { Country.EE, new EstoniaValidator() },
                { Country.ES, new SpainValidator() },
                { Country.FI, new FinlandValidator() },
                { Country.FO, new FaroeIslandsValidator() },
                { Country.FR, new FranceValidator() },
                { Country.GB, new UnitedKingdomValidator() },
                { Country.GE, new GeorgiaValidator() },
                { Country.GR, new GreeceValidator() },
                { Country.GT, new GuatemalaValidator() },
                { Country.HK, new HongKongValidator() },
                { Country.HR, new CroatiaValidator() },
                { Country.HU, new HungaryValidator() },
                { Country.ID, new IndonesiaValidator() },
                { Country.IE, new IrelandValidator() },
                { Country.IL, new IsraelValidator() },
                { Country.IN, new IndiaValidator() },
                { Country.IS, new IcelandValidator() },
                { Country.IT, new ItalyValidator()},
                { Country.JP, new JapanValidator() },
                { Country.KR, new KoreaValidator() },
                { Country.KZ, new KazahstanValidator() },
                { Country.LT, new LithuaniaValidator() },
                { Country.LU, new LuxembourgValidator() },
                { Country.LV, new LatviaValidator() },
                { Country.MC, new MonacoValidator() },
                { Country.MD, new MoldovaValidator() },
                { Country.ME, new MontenegroValidator() },
                { Country.MK, new MacedoniaValidator() },
                { Country.MT, new MaltaValidator() },
                { Country.MU, new MauritiusValidator() },
                { Country.MX, new MexicoValidator() },
                { Country.MY, new MalaysiaValidator() },
                { Country.NG, new NigeriaValidator() },
                { Country.NL, new NetherlandsValidator() },
                { Country.NO, new NorwayValidator() },
                { Country.NZ, new NewZealandValidator() },
                { Country.PE, new PeruValidator() },
                { Country.PH, new PhilippinesValidator() },
                { Country.PK, new PakistanValidator() },
                { Country.PL, new PolandValidator() },
                { Country.PT, new PortugalValidator() },
                { Country.PY, new ParaguayValidator() },
                { Country.RO, new RomaniaValidator() },
                { Country.RS, new SerbiaValidator() },
                { Country.RU, new RussiaValidator() },
                { Country.SE, new SwedenValidator() },
                { Country.SI, new SloveniaValidator() },
                { Country.SK, new SlovakiaValidator() },
                { Country.SM, new SanMarinoValidator() },
                { Country.SV, new ElSalvadorValidator() },
                { Country.TH, new ThailandValidator() },
                { Country.TR, new TurkeyValidator() },
                { Country.TW, new TaiwanValidator() },
                { Country.UA, new UkraineValidator() },
                { Country.US, new UnitedStatesValidator() },
                { Country.UY, new UruguayValidator() },
                { Country.UZ, new UzbekistanValidator() },
                { Country.VE, new VenezuelaValidator() },
                { Country.ZA, new SouthAfricaValidator() }
            };

            return ssnCountries;
        }

        public ValidationResult ValidateIndividualTaxCode(string ssn, string countryCode)
        {
            if (TryGetCountryByCode(countryCode, out Country country))
                return ValidateIndividualTaxCode(ssn, country);
            else
                return ValidationResult.CountryNotSupported(countryCode);
        }
        public ValidationResult ValidateIndividualTaxCode(string ssn, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateIndividualTaxCode(ssn);
            }
            return ValidationResult.CountryNotSupported(country);
        }

        public ValidationResult ValidateVAT(string vat, string countryCode)
        {
            if (TryGetCountryByCode(countryCode,out Country country))
                return ValidateVAT(vat, country);
            else
                return ValidationResult.CountryNotSupported(countryCode);
        }
        public ValidationResult ValidateVAT(string vat, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateVAT(vat);
            }
            return ValidationResult.CountryNotSupported(country);
        }

        public ValidationResult ValidateEntity(string vat, string countryCode)
        {
            if (TryGetCountryByCode(countryCode, out Country country))
                return ValidateEntity(vat, country);
            else
                return ValidationResult.CountryNotSupported(countryCode);
        }
        public ValidationResult ValidateEntity(string vat, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateEntity(vat);
            }
            return ValidationResult.CountryNotSupported(country);
        }

        public ValidationResult ValidateNationalIdentityCode(string ssn, string countryCode)
        {
            if (TryGetCountryByCode(countryCode, out Country country))
                return ValidateNationalIdentityCode(ssn, country);
            else
                return ValidationResult.CountryNotSupported(countryCode);
        }
        public ValidationResult ValidateNationalIdentityCode(string ssn, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidateNationalIdentity(ssn);
            }
            return ValidationResult.CountryNotSupported(country);
        }

        public ValidationResult ValidateZIPCode(string zip, string countryCode)
        {
            if (TryGetCountryByCode(countryCode, out Country country))
                return ValidateZIPCode(zip, country);
            else
                return ValidationResult.CountryNotSupported(countryCode);
        }
        public ValidationResult ValidateZIPCode(string zip, Country country)
        {
            if (_supportedCountries.ContainsKey(country))
            {
                return _supportedCountries[country].ValidatePostalCode(zip);
            }
            return ValidationResult.CountryNotSupported(country);
        }
    }
}
