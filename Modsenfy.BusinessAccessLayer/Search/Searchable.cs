using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modsenfy.BusinessAccessLayer.Search;

public class Searchable
{
    private float _rate = -1f;
    public object SearchObject { get; }
    public PropertyInfo SearchProperty { get; }
    public string Query { get; }
    public float Rate
    {
        get
        {
            if (_rate == -1f)
                _rate = CountRate();
            return _rate;
        }
    }

    public Searchable(object searchObject, PropertyInfo searchProperty, string query)
    {
        SearchObject = searchObject;
        SearchProperty = searchProperty;
        Query = query;
    }

    private float CountRate()
    {
        float rate = 0f;

        var queryWords = Query.ToLowerInvariant().Split(new char[] { ' ' });
        var nameWords = ((string)SearchProperty.GetValue(SearchObject)).ToLowerInvariant().Split(new char[] { ' ' });

        List<float> rateTerms = new List<float>();
        foreach (var nameWord in nameWords)
        {
            foreach (var queryWord in queryWords)
            {
                while (!nameWord.Contains(queryWord))
                {
                    queryWord.Remove(queryWord.Length - 1);
                }
                rateTerms.Add(queryWord.Length / nameWord.Length);
            }
        }

        foreach (var rateTerm in rateTerms)
            rate += rateTerm;
        rate /= rateTerms.Count;
        return rate;
    }
}
